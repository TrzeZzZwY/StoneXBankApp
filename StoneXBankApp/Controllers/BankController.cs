using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoneXBankApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoneXBankApp.Controllers
{
    public class BankController : Controller
    {
        public const string SessionKeyName = "_Id";
        public IActionResult Home()
        {
            var id = HttpContext.Session.GetString(SessionKeyName);
            if (!String.IsNullOrEmpty(id))
            {
                Profile profile = new Profile();
                using (var db = new MyDbContext())
                {
                    profile = (from p in db.profiles
                               where p.Id == int.Parse(id)
                               select p).FirstOrDefault();
                }
                if (profile != null)
                    return View("Index", profile);
            }

            return View();
        }
        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetString(SessionKeyName);
            if (!String.IsNullOrEmpty(id))
            {
                Profile profile = FindPerson(int.Parse(id));
                if (profile != null)
                    return View(profile);
            }
            return BadRequest();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString(SessionKeyName, String.Empty);
            return View("Home");
        }
        public IActionResult Index()
        {
            var id = HttpContext.Session.GetString(SessionKeyName);
            if (!String.IsNullOrEmpty(id))
            {
                Profile profile = new Profile();
                using (var db = new MyDbContext())
                {
                    profile = (from p in db.profiles
                               where p.Id == int.Parse(id)
                               select p).FirstOrDefault();
                }
                return View(profile);
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            Profile profile = new Profile();
            bool loginSucces = false;
            using (var mydb = new MyDbContext())
            {
                profile = (from p in mydb.profiles
                           where p.Login == loginViewModel.Login
                           select p).FirstOrDefault();

                if (profile != null && loginViewModel.Password == profile.Password)
                    loginSucces = true;

            }
            if (loginSucces)
            {
                HttpContext.Session.SetString(SessionKeyName, profile.Id.ToString());
                return View("Index", profile);
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register([FromForm] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid || !registerViewModel.RegulaminAccepted)
                return View();
            Profile newProfile = new Profile()
            {
                Name = registerViewModel.Name,
                Surename = registerViewModel.Surename,
                Login = registerViewModel.Login,
                Password = registerViewModel.Password,
                BirtDate = registerViewModel.BirthDate,
                Pesel = registerViewModel.Pesel,
                Email = registerViewModel.Email,
                CreateDate = DateTime.Now
            };
            using (var db = new MyDbContext())
            {
                db.profiles.Add(newProfile);
                db.SaveChanges();
            }
            HttpContext.Session.SetString(SessionKeyName, newProfile.Id.ToString());
            return View("index", newProfile);
        }

        public IActionResult ChangePassword()
        {
            var id = HttpContext.Session.GetString(SessionKeyName);
            if (!String.IsNullOrEmpty(id))
            {
                return View();
            }
            else
                return BadRequest();
        }
        [HttpPost]
        public IActionResult ChangePassword([FromForm] ChangePasswordViewModel change)
        {
            var id = HttpContext.Session.GetString(SessionKeyName);
            if (ModelState.IsValid
                && FindPerson(int.Parse(id)).Password == change.OldPassword
                && change.NewPassword == change.NewPasswordRepeated)
            {
                using (var db = new MyDbContext())
                {
                    Profile profile = (from p in db.profiles
                                       where p.Id == int.Parse(id)
                                       select p).FirstOrDefault();
                    profile.Password = change.NewPassword;
                    db.profiles.Update(profile);
                    db.SaveChanges();
                }
                return View("index", FindPerson(int.Parse(id)));
            }
            return View();
        }

        public IActionResult BankAccountsList()
        {
            var id = HttpContext.Session.GetString(SessionKeyName);
            if (!String.IsNullOrEmpty(id))
            {
                List<BankAccount> bankAccounts = new List<BankAccount>();
                using (var db = new MyDbContext())
                {
                    Profile profile = (from p in db.profiles
                                       where p.Id == int.Parse(id)
                                       select p).FirstOrDefault();

                    List<Currency> curr = (from c in db.currencies
                                           select c).ToList();

                    bankAccounts = (from ba in db.bankAccounts
                                    where (ba.Profile == profile
                                    && curr.Contains(ba.Currency))
                                    select ba).ToList();
                }
                return View("BankAccountsList", bankAccounts);
            }
            return View("Login");
        }

        public IActionResult BankAccountForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BankAccountForm([FromForm] BankAccountViewModel bankAccountViewModel)
        {
            if (!ModelState.IsValid)
                return View();
            var id = HttpContext.Session.GetString(SessionKeyName);
            if (String.IsNullOrEmpty(id))
                return View("Login");
            BankAccount bankAccount = new BankAccount()
            {
                Balance = 0,
                CrateDate = DateTime.Now,
            };

            List<BankAccount> bankAccounts = new List<BankAccount>();

            using (var db = new MyDbContext())
            {
                bankAccount.Currency = db.currencies.Where(e => e.Name == bankAccountViewModel.Currency).FirstOrDefault();
                bankAccount.Profile = db.profiles.Where(e => e.Id == int.Parse(id)).FirstOrDefault();
                var numbers = db.bankAccounts.Where(e => e.Profile.Id == int.Parse(id))
                                             .Select(e => e.Id)
                                             .ToList();
                int number = numbers.Count == 0 ? 0 : numbers.Max();

                bankAccount.BankAccountNumber = $"60{bankAccount.Currency.Id}{bankAccount.Profile.Id}{number}";
                db.bankAccounts.Add(bankAccount);
                db.SaveChanges();
                bankAccounts = (from ba in db.bankAccounts
                                join p in db.profiles on ba.Profile.Id equals p.Id
                                where p.Id == int.Parse(id)
                                select ba).ToList();
            }
            return BankAccountsList();
        }

        public IActionResult BankAccountDetails([FromRoute] int? id)
        {
            var profileId = HttpContext.Session.GetString(SessionKeyName);
            BankAccountDetailsViewModel detailsViewModel = new BankAccountDetailsViewModel();
            using (var db = new MyDbContext())
            {
                Profile profile = (from p in db.profiles
                                   where p.Id == int.Parse(profileId)
                                   select p).FirstOrDefault();

                List<Currency> curr = (from c in db.currencies
                                       select c).ToList();

                var test = (from a in db.bankAccounts
                            select a).ToList();

                BankAccount bankAccount = (from ba in db.bankAccounts
                                           where (ba.Profile == profile
                                           && curr.Contains(ba.Currency)
                                           && ba.Id == id)
                                           select ba).FirstOrDefault();

                detailsViewModel.Currency = bankAccount.Currency;
                detailsViewModel.BankAccountNumber = bankAccount.BankAccountNumber;
                detailsViewModel.Balance = bankAccount.Balance;

                detailsViewModel.Transactions = (from t in db.transactions
                                                 where t.sender == bankAccount
                                                 select t).ToList();

            }


            return View("BankAccountDetails", detailsViewModel);
        }
        public IActionResult BankAccountTransactionForm([FromRoute] string? id)
        {
            var profileId = HttpContext.Session.GetString(SessionKeyName);
            TransactionViewModel transaction = new TransactionViewModel();
            using (var db = new MyDbContext())
            {
                Profile profile = (from p in db.profiles
                                   where p.Id == int.Parse(profileId)
                                   select p).FirstOrDefault();

                transaction.SenderBankAccountNumber = (from ba in db.bankAccounts
                                                       where (ba.Profile == profile
                                                       && ba.BankAccountNumber == id)
                                                       select ba.BankAccountNumber).FirstOrDefault();
            }
            return View(transaction);
        }

        [HttpPost]
        public IActionResult BankAccountTransactionForm([FromForm] TransactionViewModel transactionViewModel)
        {
            if (!ModelState.IsValid)
                return View(transactionViewModel);
            BankAccount? Sender = new BankAccount();
            BankAccount? Recipient = new BankAccount();

            using (var db = new MyDbContext())
            {
                List<Currency> curr = (from c in db.currencies
                                       select c).ToList();
                Sender = (from ba in db.bankAccounts
                          where (ba.BankAccountNumber == transactionViewModel.SenderBankAccountNumber
                          && curr.Contains(ba.Currency))
                          select ba).FirstOrDefault();
                Recipient = (from ba in db.bankAccounts
                             where (ba.BankAccountNumber == transactionViewModel.RecipientBankAccountNumber
                              && curr.Contains(ba.Currency))
                             select ba).FirstOrDefault();

                if (Sender is null || Recipient is null || Sender.Balance < transactionViewModel.Amout || Sender.Currency != Recipient.Currency)
                    return View(transactionViewModel);
                BankAccountDetailsViewModel detailsViewModel = new BankAccountDetailsViewModel();

                Sender.Balance -= transactionViewModel.Amout;
                Recipient.Balance += transactionViewModel.Amout;
                Transaction transaction = new Transaction()
                {
                    sender = Sender,
                    recipient = Recipient,
                    Amout = transactionViewModel.Amout
                };
                db.transactions.Add(transaction);
                db.bankAccounts.Update(Sender);
                db.bankAccounts.Update(Recipient);
                db.SaveChanges();

                detailsViewModel.Balance = Sender.Balance;
                detailsViewModel.BankAccountNumber = Sender.BankAccountNumber;
                detailsViewModel.Currency = Sender.Currency;
                detailsViewModel.Transactions = (from t in db.transactions
                                                 where t.sender == Sender
                                                 select t).ToList();
                return BankAccountDetails(Sender.Id);
            }
        }

        public IActionResult BankAccountTransactionTopUpForm([FromRoute] string? id)
        {
            var profileId = HttpContext.Session.GetString(SessionKeyName);
            TransactionTopUpViewModel transaction = new TransactionTopUpViewModel();
            using (var db = new MyDbContext())
            {
                Profile profile = (from p in db.profiles
                                   where p.Id == int.Parse(profileId)
                                   select p).FirstOrDefault();

                transaction.SenderBankAccountNumber = (from ba in db.bankAccounts
                                                       where (ba.Profile == profile
                                                       && ba.BankAccountNumber == id)
                                                       select ba.BankAccountNumber).FirstOrDefault();
            }
            return View(transaction);
        }
        [HttpPost]
        public IActionResult BankAccountTransactionTopUpForm([FromForm] TransactionTopUpViewModel transactionViewModel)
        {
            if (!ModelState.IsValid)
                return View(transactionViewModel);
            BankAccount? Sender = new BankAccount();

            using (var db = new MyDbContext())
            {
                List<Currency> curr = (from c in db.currencies
                                       select c).ToList();
                Sender = (from ba in db.bankAccounts
                          where (ba.BankAccountNumber == transactionViewModel.SenderBankAccountNumber
                          && curr.Contains(ba.Currency))
                          select ba).FirstOrDefault();

                if (Sender is null)
                    return View(transactionViewModel);
                BankAccountDetailsViewModel detailsViewModel = new BankAccountDetailsViewModel();



                Sender.Balance += transactionViewModel.Amout;
                Transaction transaction = new Transaction()
                {
                    sender = Sender,
                    Amout = transactionViewModel.Amout
                };
                db.Update(Sender);
                db.SaveChanges();

                detailsViewModel.Balance = Sender.Balance;
                detailsViewModel.BankAccountNumber = Sender.BankAccountNumber;
                detailsViewModel.Currency = Sender.Currency;
                detailsViewModel.Transactions = (from t in db.transactions
                                                 where t.sender == Sender
                                                 select t).ToList();
                return BankAccountDetails(Sender.Id);
            }
        }
        private Profile FindPerson(int id)
        {
            Profile profile = new Profile();
            using (var db = new MyDbContext())
            {
                profile = (from p in db.profiles
                           where p.Id == id
                           select p).FirstOrDefault();
            }
            return profile;
        }

    }
}

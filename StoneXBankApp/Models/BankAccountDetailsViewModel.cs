namespace StoneXBankApp.Models
{
    public class BankAccountDetailsViewModel
    {
        public string BankAccountNumber { get; set; }

        public decimal Balance { get; set; } = 0;
        public virtual Currency Currency { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}

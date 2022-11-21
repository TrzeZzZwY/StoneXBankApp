using System.ComponentModel.DataAnnotations;

namespace StoneXBankApp.Models
{
    public class RegisterViewModel
    {
        /*
         * imię
         * nazwisko
         * email
         * login
         * hasło
         * data urodzenia
         * pesel
         * zgody na regulamin
         */

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surename { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Pesel { get; set; }
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Active must be checked.")]
        public bool RegulaminAccepted { get; set; }

    }
}

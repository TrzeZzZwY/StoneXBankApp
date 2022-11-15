using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StoneXBankApp.Models
{
    public class BankAccount
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public int ProfileId { get; set; }

        [Required]
        public string BankAccountNumber { get; set; }

        [Required]
        [HiddenInput]
        public DateTime CrateDate { get; set; }

        [Required]
        [HiddenInput]
        public decimal Balance { get; set; } = 0;

        [Required]
        [HiddenInput]
        public int CurrencyId { get; set; }

        [Required]
        [HiddenInput]
        public Currency Currency { get; set; }
    }
}

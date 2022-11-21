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
        public virtual Profile Profile { get; set; }

        [Required]
        //[RegularExpression("[0-9]{18}")]
        public string BankAccountNumber { get; set; }

        [Required]
        [HiddenInput]
        public DateTime CrateDate { get; set; }

        [Required]
        [HiddenInput]
        public decimal Balance { get; set; } = 0;

        [Required]
        [HiddenInput]
        public virtual Currency Currency { get; set; }
    }
}

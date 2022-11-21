using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StoneXBankApp.Models
{
    public class Transaction
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public BankAccount sender { get; set; }
        [Required]
        public BankAccount recipient { get; set; }

        [Required]
        public decimal Amout { get; set; }
    }
}

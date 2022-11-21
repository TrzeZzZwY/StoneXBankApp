using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StoneXBankApp.Models
{
    public class TransactionViewModel
    {

        [HiddenInput]
        public string SenderBankAccountNumber { get; set; }
        [Required]
        public string RecipientBankAccountNumber { get; set; }

        [Required]
        public decimal Amout { get; set; }
    }
}

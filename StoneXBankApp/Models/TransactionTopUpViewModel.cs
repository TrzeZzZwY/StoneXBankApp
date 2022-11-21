using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StoneXBankApp.Models
{
    public class TransactionTopUpViewModel
    {
        [HiddenInput]
        public string SenderBankAccountNumber { get; set; }

        [Required]
        public decimal Amout { get; set; }
    }
}

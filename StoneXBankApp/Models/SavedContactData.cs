using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StoneXBankApp.Models
{
    public class SavedContactData
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        //[RegularExpression(@"[a-z]{2,}")]
        public string Name { get; set; }

        [Required]
        //[RegularExpression(@"[a-z]{2,}")]
        public string Surename { get; set; }

        //[RegularExpression("[0-9]*")]
        public string BankAccountNumber { get; set; }
    }
}

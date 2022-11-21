using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using StoneXBankApp.Models.MyAttributes;
    
namespace StoneXBankApp.Models
{
    public class Profile
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

        [Required]
        ////[RegularExpression(@"[a-z0-9]+@[a-z]+\.[a-z]{2,3}")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
       // [RegularExpression(@"[a-zA-Z0-9]{5,30}")]
        public string Login { get; set; }

        [Required]
        //[RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }

        [Required]
        //[RegularExpression("[0-9]{11}")]
        public string Pesel { get; set; }

        [Required]
        [HaveMoreYearsThan(16)]
        public DateTime BirtDate { get; set; }

        [HiddenInput]
        public DateTime CreateDate { get; set; }

        [HiddenInput]
        public ICollection<SavedContactData> savedContactData { get; set; }

    }
}

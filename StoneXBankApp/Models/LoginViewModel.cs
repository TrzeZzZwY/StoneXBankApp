using System.ComponentModel.DataAnnotations;

namespace StoneXBankApp.Models
{
    public class LoginViewModel
    {
        [Required]
        // [RegularExpression(@"[a-zA-Z0-9]{5,30}")]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string? Password { get; set; }
    }
}

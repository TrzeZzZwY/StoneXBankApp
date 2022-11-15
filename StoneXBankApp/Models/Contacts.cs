using System.ComponentModel.DataAnnotations;
namespace StoneXBankApp.Models
{
    public class Contacts
    {

        [Key]
        public int ProfileId { get; set; }
        [Key]
        public int BankAccountId { get; set; }
    }
}

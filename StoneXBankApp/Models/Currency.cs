using System.ComponentModel.DataAnnotations;

namespace StoneXBankApp.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

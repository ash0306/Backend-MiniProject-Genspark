using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Customer
{
    public class UpdatePhoneDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string Phone { get; set; }
    }
}

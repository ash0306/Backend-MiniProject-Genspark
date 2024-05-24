using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Customer
{
    public class UpdatePhoneDTO
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string Phone { get; set; }
    }
}

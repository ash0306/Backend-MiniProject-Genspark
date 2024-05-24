using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Customer
{
    public class CustomerDTO
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public int LoyaltyPoints { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Customer
{
    public class LoyaltyPointsDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int LoyaltyPoints { get; set; }
    }
}

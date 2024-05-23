using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Customer
{
    public class CustomerLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}

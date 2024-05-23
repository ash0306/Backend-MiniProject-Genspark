using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Employee
{
    public class EmployeeLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}

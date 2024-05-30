using CoffeeStoreApplication.Validations;
using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Employee
{
    public class EmployeeRegisterDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string Phone { get; set; }

        [Required]
        [DateTimeAgeValidation]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MinLength(7)]
        public string Password { get; set; }
    }
}

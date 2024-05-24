using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Validations;
using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Employee
{
    public class EmployeeSalaryDTO
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public float? EmployeeSalary { get; set; }
    }
}

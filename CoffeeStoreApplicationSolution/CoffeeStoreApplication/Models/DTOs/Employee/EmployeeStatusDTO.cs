using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Validations;
using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Employee
{
    public class EmployeeStatusDTO
    {
        [Required]
        public int EmployeeId { get; set; }

        [EnumValidation(typeof(EmployeeStatus))]
        public string EmployeeStatus { get; set; }
    }
}

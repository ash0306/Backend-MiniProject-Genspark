using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Employee
{
    public class EmployeeStatusDTO
    {
        [Required]
        public int EmployeeId { get; set; }

        
        public string EmployeeStatus { get; set; }
    }
}

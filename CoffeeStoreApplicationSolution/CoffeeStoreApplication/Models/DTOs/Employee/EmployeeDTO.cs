using CoffeeStoreApplication.Models.Enum;

namespace CoffeeStoreApplication.Models.DTOs.Employee
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public float? Salary { get; set; }
        public string Role { get; set; }
    }
}

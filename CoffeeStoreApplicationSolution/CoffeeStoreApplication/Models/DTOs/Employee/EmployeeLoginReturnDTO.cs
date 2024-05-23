namespace CoffeeStoreApplication.Models.DTOs.Employee
{
    public class EmployeeLoginReturnDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}

using CoffeeStoreApplication.Models;

namespace CoffeeStoreApplication.Interfaces
{
    public interface ITokenService
    {
        public string GenerateCustomerToken(Customer customer);
        public string GenerateEmployeeToken(Employee employee);
    }
}

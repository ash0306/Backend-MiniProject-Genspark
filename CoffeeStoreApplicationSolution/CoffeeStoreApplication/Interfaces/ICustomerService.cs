using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;

namespace CoffeeStoreApplication.Interfaces
{
    public interface ICustomerService
    {
        public Task<IEnumerable<CustomerDTO>> GetAllCustomers();
        public Task<CustomerDTO> GetCustomerById(int id);
        public Task<LoyaltyPointsDTO> UpdateLoyaltyPoints(LoyaltyPointsDTO loyaltyPoints);
        public Task<UpdatePhoneDTO> UpdatePhone(UpdatePhoneDTO updatePhoneDTO);
    }
}

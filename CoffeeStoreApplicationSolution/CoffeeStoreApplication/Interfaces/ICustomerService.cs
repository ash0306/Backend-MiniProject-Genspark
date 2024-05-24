using CoffeeStoreApplication.Models.DTOs.Customer;

namespace CoffeeStoreApplication.Interfaces
{
    public interface ICustomerService
    {
        public Task<LoyaltyPointsDTO> UpdateLoyaltyPoints(LoyaltyPointsDTO loyaltyPoints);
        public Task<UpdatePhoneDTO> UpdatePhone(UpdatePhoneDTO updatePhoneDTO);
    }
}

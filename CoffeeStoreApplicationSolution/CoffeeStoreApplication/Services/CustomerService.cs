using CoffeeStoreApplication.Exceptions.CustomerExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;

namespace CoffeeStoreApplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<int, Customer> _repository;

        public CustomerService(IRepository<int, Customer> repository)
        {
            _repository = repository;
        }
        public async Task<LoyaltyPointsDTO> UpdateLoyaltyPoints(LoyaltyPointsDTO loyaltyPoints)
        {
            Customer customer = await _repository.GetById(loyaltyPoints.CustomerId);
            if(customer == null)
            {
                throw new NoSuchCustomerException($"No customer with ID {loyaltyPoints.CustomerId} exists");
            }

            customer.LoyaltyPoints = (customer.LoyaltyPoints + loyaltyPoints.LoyaltyPoints);
            
            var updatedCustomer = await _repository.Update(customer);
            loyaltyPoints = new LoyaltyPointsDTO()
            {
                CustomerId = updatedCustomer.Id,
                LoyaltyPoints = updatedCustomer.LoyaltyPoints
            };

            return loyaltyPoints;
        }

        public async Task<UpdatePhoneDTO> UpdatePhone(UpdatePhoneDTO updatePhoneDTO)
        {
            Customer customer = await _repository.GetById(updatePhoneDTO.CustomerId);
            if (customer == null)
            {
                throw new NoSuchCustomerException($"No customer with ID {updatePhoneDTO.CustomerId} exists");
            }

            customer.Phone = updatePhoneDTO.Phone;

            var updatedCustomer = await _repository.Update(customer);
            updatePhoneDTO = new UpdatePhoneDTO()
            {
                CustomerId = updatedCustomer.Id,
                Phone = updatedCustomer.Phone
            };

            return updatePhoneDTO;
        }
    }
}

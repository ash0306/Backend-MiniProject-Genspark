using AutoMapper;
using CoffeeStoreApplication.Exceptions.CustomerExceptions;
using CoffeeStoreApplication.Exceptions.EmployeeExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.DTOs.Employee;

namespace CoffeeStoreApplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<int, Customer> _repository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<int, Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            var customers = await _repository.GetAll();

            if (customers.Count() == 0)
            {
                throw new NoCustomersFoundException("No customers found");
            }

            IList<CustomerDTO> result = new List<CustomerDTO>();

            foreach (var customer in customers)
            {
                result.Add(_mapper.Map<CustomerDTO>(customer));
            }
            return result;
        }

        public async Task<CustomerDTO> GetCustomerByEmail(string email)
        {
            var customer = (await _repository.GetAll()).FirstOrDefault(c => c.Email == email);
            if(customer == null)
            {
                throw new NoSuchCustomerException($"No customer with email {email} found");
            }
            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            var customer = await _repository.GetById(id);

            if (customer == null)
            {
                throw new NoSuchCustomerException($"No customer with ID {id} exists");
            }

            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
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

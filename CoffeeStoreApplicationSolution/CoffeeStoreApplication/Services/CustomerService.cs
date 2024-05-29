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
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IRepository<int, Customer> repository, IMapper mapper, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of all the customers
        /// </summary>
        /// <returns>List of all customers</returns>
        /// <exception cref="NoCustomersFoundException">If no customers are found</exception>
        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            var customers = await _repository.GetAll();

            if (customers.Count() == 0)
            {
                _logger.LogError("No customers found");
                throw new NoCustomersFoundException("No customers found");
            }

            IList<CustomerDTO> result = new List<CustomerDTO>();

            foreach (var customer in customers)
            {
                result.Add(_mapper.Map<CustomerDTO>(customer));
            }
            return result;
        }

        /// <summary>
        /// Get Customer details usng Email
        /// </summary>
        /// <param name="email">Email of the customer to be found</param>
        /// <returns></returns>
        /// <exception cref="NoSuchCustomerException"></exception>
        public async Task<CustomerDTO> GetCustomerByEmail(string email)
        {
            var customer = (await _repository.GetAll()).FirstOrDefault(c => c.Email == email);
            if(customer == null)
            {
                _logger.LogError("No customers found");
                throw new NoSuchCustomerException($"No customer with email {email} found");
            }
            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        /// <summary>
        /// Get Customer details using ID.
        /// </summary>
        /// <param name="id">ID of the customer to be found</param>
        /// <returns>CustomerDTO object containing customer details</returns>
        /// <exception cref="NoSuchCustomerException">If no customer with the specified ID exists</exception>
        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            var customer = await _repository.GetById(id);

            if (customer == null)
            {
                _logger.LogError("No customers found");
                throw new NoSuchCustomerException($"No customer with ID {id} exists");
            }

            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        /// <summary>
        /// Update the loyalty points of a customer.
        /// </summary>
        /// <param name="loyaltyPoints">LoyaltyPointsDTO object containing customer ID and new loyalty points</param>
        /// <returns>Updated LoyaltyPointsDTO object</returns>
        /// <exception cref="NoSuchCustomerException">If no customer with the specified ID exists</exception>
        public async Task<LoyaltyPointsDTO> UpdateLoyaltyPoints(LoyaltyPointsDTO loyaltyPoints)
        {
            Customer customer = (await _repository.GetAll()).FirstOrDefault(c=>c.Email == loyaltyPoints.Email);
            if(customer == null)
            {
                _logger.LogError("No customer found");
                throw new NoSuchCustomerException($"No customer with email {loyaltyPoints.Email} exists");
            }

            customer.LoyaltyPoints = (customer.LoyaltyPoints + loyaltyPoints.LoyaltyPoints);
            
            var updatedCustomer = await _repository.Update(customer);
            loyaltyPoints = new LoyaltyPointsDTO()
            {
                Email = updatedCustomer.Email,
                LoyaltyPoints = updatedCustomer.LoyaltyPoints
            };

            return loyaltyPoints;
        }

        /// <summary>
        /// Update the phone number of a customer.
        /// </summary>
        /// <param name="updatePhoneDTO">UpdatePhoneDTO object containing customer ID and new phone number</param>
        /// <returns>Updated UpdatePhoneDTO object</returns>
        /// <exception cref="NoSuchCustomerException">If no customer with the specified ID exists</exception>
        public async Task<UpdatePhoneDTO> UpdatePhone(UpdatePhoneDTO updatePhoneDTO)
        {
            Customer customer = (await _repository.GetAll()).FirstOrDefault(c=>c.Email == updatePhoneDTO.Email);
            if (customer == null)
            {
                _logger.LogError("No customer found");
                throw new NoSuchCustomerException($"No customer with email {updatePhoneDTO.Email} exists");
            }

            customer.Phone = updatePhoneDTO.Phone;

            var updatedCustomer = await _repository.Update(customer);
            updatePhoneDTO = new UpdatePhoneDTO()
            {
                Email = updatedCustomer.Email,
                Phone = updatedCustomer.Phone
            };

            return updatePhoneDTO;
        }
    }
}

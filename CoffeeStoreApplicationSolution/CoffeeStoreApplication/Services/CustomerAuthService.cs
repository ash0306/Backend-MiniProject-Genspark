using AutoMapper;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Exceptions.CustomerExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.Enum;
using System.Security.Cryptography;
using System.Text;

namespace CoffeeStoreApplication.Services
{
    public class CustomerAuthService : IAuthLoginService<CustomerLoginReturnDTO, CustomerLoginDTO>, IAuthRegisterService<CustomerRegisterReturnDTO, CustomerRegisterDTO>
    {
        private readonly IRepository<int, Customer> _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public CustomerAuthService(IRepository<int, Customer> repository, ITokenService tokenService, IMapper mapper)
        {
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<CustomerRegisterReturnDTO> Register(CustomerRegisterDTO registerDTO, RoleType role)
        {
            Customer customer;

            try
            {
                var alreadyPresent = (await _repository.GetAll()).FirstOrDefault(c=>c.Email == registerDTO.Email);
                if(alreadyPresent != null)
                {
                    throw new UserAlreadyExistsException($"User with this Email already exists");
                }

                customer = _mapper.Map<Customer>(registerDTO);
                HMACSHA512 hMACSHA = new HMACSHA512();

                customer.PasswordHashKey = hMACSHA.Key;
                customer.HashedPassword = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));

                var newCustomer = await _repository.Add(customer);

                CustomerRegisterReturnDTO returnDTO = _mapper.Map<CustomerRegisterReturnDTO>(newCustomer);
                
                return returnDTO;
            }
            catch
            {
                throw new UnableToRegisterException($"Unable to register at the moment");
            }
        }

        public async Task<CustomerLoginReturnDTO> Login(CustomerLoginDTO loginDTO)
        {
            Customer customer = (await _repository.GetAll()).FirstOrDefault(c => c.Email == loginDTO.Email);
            if(customer == null)
            {
                throw new UnauthorizedUserException("Invalid email or password");
            }

            HMACSHA512 hMACSHA512 = new HMACSHA512(customer.PasswordHashKey);
            var encryptedPassword = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isCorrectPassword = ComparePassword(encryptedPassword, customer.HashedPassword) ;

            if(isCorrectPassword)
            {
                CustomerLoginReturnDTO returnDTO = new CustomerLoginReturnDTO()
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Token = _tokenService.GenerateCustomerToken(customer)
                };
                return returnDTO;
            }

            throw new UnauthorizedUserException("Invalid email or password");
        }

        private bool ComparePassword(byte[] encryptedPassword, byte[] password)
        {
            for (int i = 0; i < encryptedPassword.Length; i++)
            {
                if (encryptedPassword[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

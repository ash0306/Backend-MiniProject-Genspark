using AutoMapper;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Exceptions.CustomerExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.Enum;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace CoffeeStoreApplication.Services
{
    public class CustomerAuthService : IAuthLoginService<CustomerLoginReturnDTO, CustomerLoginDTO>, IAuthRegisterService<CustomerRegisterReturnDTO, CustomerRegisterDTO>
    {
        private readonly IRepository<int, Customer> _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerAuthService> _logger;

        public CustomerAuthService(IRepository<int, Customer> repository, ITokenService tokenService, IMapper mapper, ILogger<CustomerAuthService> logger)
        {
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Register a customer
        /// </summary>
        /// <param name="registerDTO">Gets the user details for registration</param>
        /// <param name="role">Role of the user to be register</param>
        /// <returns>The details of the registered user</returns>
        /// <exception cref="UnableToRegisterException">Thrown if unable to register at the moment</exception>
        public async Task<CustomerRegisterReturnDTO> Register(CustomerRegisterDTO registerDTO, RoleType role)
        {
            Customer customer;

            try
            {
                var alreadyPresent = (await _repository.GetAll()).FirstOrDefault(c=>c.Email == registerDTO.Email);
                if(alreadyPresent != null)
                {
                    _logger.LogCritical("User with Email already exists");
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
            catch(Exception ex) 
            {
                _logger.LogCritical("Could not Register");
                throw new UnableToRegisterException(ex.Message);
            }
        }

        /// <summary>
        /// Login the customer
        /// </summary>
        /// <param name="loginDTO">Email and password for customer login</param>
        /// <returns>Returns the details of the logged in user</returns>
        /// <exception cref="UnauthorizedUserException">Thrown if the email or password given by user is invalid</exception>
        public async Task<CustomerLoginReturnDTO> Login(CustomerLoginDTO loginDTO)
        {
            Customer customer = (await _repository.GetAll()).FirstOrDefault(c => c.Email == loginDTO.Email);
            if(customer == null)
            {
                _logger.LogCritical("Could not Login");
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
            _logger.LogCritical("Could not Login");
            throw new UnauthorizedUserException("Invalid email or password");
        }

        /// <summary>
        /// Compare the passwords of the user in DB and the password entered by user
        /// </summary>
        /// <param name="encryptedPassword">Hashed value of the password entered by user</param>
        /// <param name="password">Hashed Password in the DB</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
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

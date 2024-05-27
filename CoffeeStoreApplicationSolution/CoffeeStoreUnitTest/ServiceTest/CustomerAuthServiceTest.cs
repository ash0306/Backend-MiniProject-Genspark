using AutoMapper;
using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Repositories;
using CoffeeStoreApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.ServiceTest
{
    public class CustomerAuthServiceTest
    {
        CoffeeStoreContext context;
        ITokenService _tokenService;
        IRepository<int, Customer> repository;
        IMapper _mapper;
        MapperConfiguration _config;

        [SetUp]
        public async Task Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                                .UseInMemoryDatabase("dummyCustomerAuthDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);
            repository = new CustomerRepository(context);
            
            _config = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "CoffeeStoreApplication" }));
            _mapper = _config.CreateMapper();

            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("Lk3xG9pVqRmZtRwYk7oPnTjWrAsDfGhUi8yBnJkLm9zXx2cVnMl0pOu1tZr4eDcFvGbHnJm5sR3Zn9JyQaPx7oWtUgXhIvDcFeGbVkLmOpNjRbEaUcPy8x6y0Zq4w1u3t5r7i9w2");
            Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
            congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);
            _tokenService = new TokenService(mockConfig.Object);


            #region SeedDatabase
            var hmac = new HMACSHA512();
            Customer customer1 = new Customer()
            {
                Name = "Test",
                DateOfBirth = DateTime.Now,
                Email = "test1@gamil.com",
                Phone = "1234566789",
                LoyaltyPoints = 0,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("testing123"))
            };
            Customer customer2 = new Customer()
            {
                Name = "Test",
                DateOfBirth = DateTime.Now,
                Email = "test2@gamil.com",
                Phone = "1234566789",
                LoyaltyPoints = 0,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("testing123"))
            };

            await repository.Add(customer1);
            await repository.Add(customer2);

            #endregion
        }

        [Test, Order(1)]
        public async Task LoginSuccessTest()
        {
            IAuthLoginService<CustomerLoginReturnDTO, CustomerLoginDTO> CustomerLoginService = new CustomerAuthService(repository, _tokenService, _mapper);

            CustomerLoginDTO CustomerLoginDTO = new CustomerLoginDTO
            {
                Email = "test1@gamil.com",
                Password = "testing123"
            };

            CustomerLoginReturnDTO result = await CustomerLoginService.Login(CustomerLoginDTO);

            Assert.IsNotNull(result);
            Assert.That(result.Email, Is.EqualTo(CustomerLoginDTO.Email));

        }

        [Test, Order(2)]
        public void LoginFailureTest()
        {

            IAuthLoginService<CustomerLoginReturnDTO, CustomerLoginDTO> CustomerLoginService = new CustomerAuthService(repository, _tokenService, _mapper);

            CustomerLoginDTO CustomerLoginDTO = new CustomerLoginDTO
            {
                Email = "Customer1@gmail.com",
                Password = "wrong password"
            };

            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await CustomerLoginService.Login(CustomerLoginDTO));

        }

        [Test, Order(3)]
        public async Task RegisterSuccessTest()
        {

            IAuthRegisterService<CustomerRegisterReturnDTO, CustomerRegisterDTO> CustomerResgiterService = new CustomerAuthService(repository, _tokenService, _mapper);

            CustomerRegisterDTO CustomerRegisterDTO = new CustomerRegisterDTO
            {
                Name = "customer2",
                Email = "customer2@gmail.com",
                DateOfBirth = new DateTime(2000, 01, 01),
                Phone = "1234567890",
                Password = "customer2"
            };

            var result = await CustomerResgiterService.Register(CustomerRegisterDTO, RoleType.Customer);

            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(CustomerRegisterDTO.Name));

        }

        [Test, Order(4)]
        public void RegisterFailureTest()
        {

            IAuthRegisterService<CustomerRegisterReturnDTO, CustomerRegisterDTO> CustomerResgiterService = new CustomerAuthService(repository, _tokenService, _mapper);

            CustomerRegisterDTO CustomerRegisterDTO = new CustomerRegisterDTO
            {
                Name = "customer2",
                Email = "customer2@gmail.com",
                DateOfBirth = new DateTime(2000, 01, 01),
                Phone = "1234567890",
                Password = "customer2"
            };

            Assert.ThrowsAsync<UnableToRegisterException>(async () => await CustomerResgiterService.Register(CustomerRegisterDTO, RoleType.Customer));

        }
    }
}

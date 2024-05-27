using AutoMapper;
using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models.DTOs.Employee;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Repositories;
using CoffeeStoreApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.ServiceTest
{
    public class EmployeeAuthServiceTest
    {
        CoffeeStoreContext context;
        ITokenService _tokenService;
        IRepository<int, Employee> repository;
        IMapper _mapper;
        MapperConfiguration _config;

        [SetUp]
        public async Task Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                                .UseInMemoryDatabase("dummyemployeeAuthDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);
            repository = new EmployeeRepository(context);

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
            Employee employee1 = new Employee()
            {
                Name = "Test",
                DateOfBirth = DateTime.Now,
                Email = "test1@gamil.com",
                Phone = "1234566789",
                Role = RoleType.Admin,
                Status = EmployeeStatus.Active,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("testing123"))
            };
            Employee employee2 = new Employee()
            {
                Name = "Test",
                DateOfBirth = DateTime.Now,
                Email = "test2@gamil.com",
                Phone = "1234566789",
                Role = RoleType.Manager,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("testing123"))
            };

            await repository.Add(employee1);
            await repository.Add(employee2);

            #endregion
        }

        [Test, Order(1)]
        public async Task LoginSuccessTest()
        {
            IAuthLoginService<EmployeeLoginReturnDTO, EmployeeLoginDTO> employeeLoginService = new EmployeeAuthService(repository, _tokenService, _mapper);

            EmployeeLoginDTO employeeLoginDTO = new EmployeeLoginDTO
            {
                Email = "test1@gamil.com",
                Password = "testing123"
            };

            EmployeeLoginReturnDTO result = await employeeLoginService.Login(employeeLoginDTO);

            Assert.IsNotNull(result);
            Assert.That(result.Email, Is.EqualTo(employeeLoginDTO.Email));

        }

        [Test, Order(2)]
        public void LoginFailureTest()
        {

            IAuthLoginService<EmployeeLoginReturnDTO, EmployeeLoginDTO> employeeLoginService = new EmployeeAuthService(repository, _tokenService, _mapper);

            EmployeeLoginDTO employeeLoginDTO = new EmployeeLoginDTO
            {
                Email = "employee1@gmail.com",
                Password = "wrong password"
            };

            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await employeeLoginService.Login(employeeLoginDTO));

        }

        [Test, Order(3)]
        public async Task RegisterSuccessTest()
        {

            IAuthRegisterService<EmployeeRegisterReturnDTO, EmployeeRegisterDTO> employeeResgiterService = new EmployeeAuthService(repository, _tokenService, _mapper);

            EmployeeRegisterDTO employeeRegisterDTO = new EmployeeRegisterDTO
            {
                Name = "employee2",
                Email = "employee2@gmail.com",
                DateOfBirth = new DateTime(2000, 01, 01),
                Phone = "1234567890",
                Password = "employee2"
            };

            var result = await employeeResgiterService.Register(employeeRegisterDTO, RoleType.Barista);

            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(employeeRegisterDTO.Name));

        }

        [Test, Order(4)]
        public void RegisterFailureTest()
        {

            IAuthRegisterService<EmployeeRegisterReturnDTO, EmployeeRegisterDTO> employeeResgiterService = new EmployeeAuthService(repository, _tokenService, _mapper);

            EmployeeRegisterDTO employeeRegisterDTO = new EmployeeRegisterDTO
            {
                Name = "employee2",
                Email = "employee2@gmail.com",
                DateOfBirth = new DateTime(2000, 01, 01),
                Phone = "1234567890",
                Password = "employee2"
            };

            Assert.ThrowsAsync<UnableToRegisterException>(async () => await employeeResgiterService.Register(employeeRegisterDTO, RoleType.Barista));

        }
    }
}

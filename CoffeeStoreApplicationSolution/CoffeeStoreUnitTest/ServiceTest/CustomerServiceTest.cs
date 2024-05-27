using AutoMapper;
using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.CustomerExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Repositories;
using CoffeeStoreApplication.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.ServiceTest
{
    public class CustomerServiceTest
    {
        CoffeeStoreContext context;
        IRepository<int, Customer> repository;
        ICustomerService customerService;
        IMapper mapper;
        MapperConfiguration mapperConfig;

        [SetUp]
        public async Task Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("dummyCustomerDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);

            mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(new[] {"CoffeeStoreApplication"}));
            mapper = mapperConfig.CreateMapper();

            repository = new CustomerRepository(context);
            customerService = new CustomerService(repository, mapper);

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
        public async Task GetAllSuccessTest()
        {
            var result = await customerService.GetAllCustomers();

            Assert.AreEqual(2, result.Count());
        }

        [Test, Order(2)]
        public async Task GetByEmailSuccessTest()
        {
            var result = await customerService.GetCustomerByEmail("test1@gamil.com");
            Assert.AreEqual("test1@gamil.com", result.Email);
        }

        [Test, Order(3)]
        public async Task GetByEmailFailureTest()
        {
            Assert.ThrowsAsync<NoSuchCustomerException>(async () => await customerService.GetCustomerByEmail("testing@gmail.com"));
        }

        [Test, Order(4)]
        public async Task GetByIdSuccessTest()
        {
            var result = await customerService.GetCustomerById(1);
            Assert.AreEqual("Test", result.Name);
        }

        [Test, Order(5)]
        public async Task GetByIdFailureTest()
        {
            Assert.ThrowsAsync<NoSuchCustomerException>(async () => await customerService.GetCustomerById(99));
        }

        [Test, Order(6)]
        public async Task UpdateLoyaltyPointsSuccessTest()
        {
            LoyaltyPointsDTO dto = new LoyaltyPointsDTO() { 
                CustomerId = 2,
                LoyaltyPoints = 20
            };
            var result = await customerService.UpdateLoyaltyPoints(dto);
            Assert.AreEqual(result.CustomerId, dto.CustomerId);
        }

        [Test, Order(7)]
        public async Task UpdateLoyaltyPointsFailureTest()
        { 
            LoyaltyPointsDTO dto = new LoyaltyPointsDTO() { 
                CustomerId = 99,
                LoyaltyPoints = 20
            };
            Assert.ThrowsAsync<NoSuchCustomerException>(async () => await customerService.UpdateLoyaltyPoints(dto));
        }

        [Test, Order(8)]
        public async Task UpdatePhoneSuccessTest()
        {
            UpdatePhoneDTO dto = new UpdatePhoneDTO()
            {
                CustomerId = 2,
                Phone = "1234567899"
            };
            var result = await customerService.UpdatePhone(dto);
            Assert.AreEqual(result.CustomerId, dto.CustomerId);
        }

        [Test, Order(9)]
        public async Task UpdatePhoneFailureTest()
        {
            UpdatePhoneDTO dto = new UpdatePhoneDTO()
            {
                CustomerId = 99,
                Phone = "1234567899"
            };
            Assert.ThrowsAsync<NoSuchCustomerException>(async () => await customerService.UpdatePhone(dto));
        }

        [Test, Order(10)]
        public async Task GetAllFailureTest()
        {
            foreach (var customer in context.Customers)
            {
                context.Customers.Remove(customer);
            }
            await context.SaveChangesAsync();

            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await customerService.GetAllCustomers());
        }
    }
}

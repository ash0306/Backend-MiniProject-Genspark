using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Exceptions.CustomerExceptions;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.RepositoryTest
{
    public class CustomerRepositoryTest
    {
        private CoffeeStoreContext _context;
        private IRepository<int, Customer> _repository;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoffeeStoreContext>()
                                  .UseInMemoryDatabase("dummyDB");
            _context = new CoffeeStoreContext(optionsBuilder.Options);
            _repository = new CustomerRepository(_context);
        }

        [Test, Order(1)]
        public async Task AddSuccessTest()
        {
            var hmac = new HMACSHA512();
            Customer customer = new Customer()
            {
                Name = "Test Customer 1",
                Email = "test1@gmail.com",
                Phone = "1234456789",
                DateOfBirth = DateTime.Now,
                LoyaltyPoints = 0,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("customer123"))
            };

            var newCustomer = await _repository.Add(customer);
            Assert.AreEqual(1, newCustomer.Id);
        }

        [Test, Order(2)]
        public void AddFailureTest()
        {
            Customer customer = null;
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Add(customer));
        }

        [Test, Order(3)]
        public async Task DeleteSuccessTest()
        {
            var hmac = new HMACSHA512();
            Customer customer = new Customer()
            {
                Name = "Test Customer 2",
                Email = "test2@gmail.com",
                Phone = "1234456789",
                DateOfBirth = DateTime.Now,
                LoyaltyPoints = 0,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("customer123"))
            };

            var newCustomer = await _repository.Add(customer);
            var deletedCustomer = await _repository.Delete(newCustomer.Id);
            Assert.AreEqual(newCustomer.Id, deletedCustomer.Id);
        }

        [Test, Order(4)]
        public void DeleteFailureTest()
        {
            Assert.ThrowsAsync<NoSuchCustomerException>(async () => await _repository.Delete(99));
        }

        [Test, Order(5)]
        public async Task GetAllSuccessTest()
        {
            var customers = await _repository.GetAll();
            Assert.IsNotEmpty(customers);
        }

        [Test, Order(6)]
        public async Task GetAllFailureTest()
        {
            // Clearing the context to ensure no customers exist
            foreach (var customer in _context.Customers)
            {
                _context.Customers.Remove(customer);
            }
            await _context.SaveChangesAsync();

            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await _repository.GetAll());
        }

        [Test, Order(7)]
        public async Task GetByIdSuccessTest()
        {
            var hmac = new HMACSHA512();
            Customer customer = new Customer()
            {
                Name = "Test Customer 3",
                Email = "test3@gmail.com",
                Phone = "1234456789",
                DateOfBirth = DateTime.Now,
                LoyaltyPoints = 0,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("customer123"))
            };

            var newCustomer = await _repository.Add(customer);
            var foundCustomer = await _repository.GetById(newCustomer.Id);
            Assert.AreEqual(newCustomer.Id, foundCustomer.Id);
        }

        [Test, Order(8)]
        public void GetByIdFailureTest()
        {
            Assert.ThrowsAsync<NoSuchCustomerException>(async () => await _repository.GetById(99));
        }

        [Test, Order(9)]
        public async Task UpdateSuccessTest()
        {
            var hmac = new HMACSHA512();
            Customer customer = new Customer()
            {
                Name = "Test Customer 4",
                Email = "test4@gmail.com",
                Phone = "1234456789",
                DateOfBirth = DateTime.Now,
                LoyaltyPoints = 0,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("customer123"))
            };

            var newCustomer = await _repository.Add(customer);
            newCustomer.Name = "Updated Customer 4";
            var updatedCustomer = await _repository.Update(newCustomer);

            Assert.AreEqual("Updated Customer 4", updatedCustomer.Name);
        }

        [Test, Order(10)]
        public void UpdateFailureTest()
        {
            var hmac = new HMACSHA512();
            Customer customer = new Customer()
            {
                Id = 99, // Assuming this ID does not exist
                Name = "Non-existent Customer",
                Email = "nonexistent@gmail.com",
                Phone = "1234456789",
                DateOfBirth = DateTime.Now,
                LoyaltyPoints = 0,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("customer123"))
            };

            Assert.ThrowsAsync<NoSuchCustomerException>(async () => await _repository.Update(customer));
        }
    }
}

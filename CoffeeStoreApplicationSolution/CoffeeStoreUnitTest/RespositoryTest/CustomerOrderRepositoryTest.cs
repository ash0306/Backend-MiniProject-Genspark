using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.CustomerOrderExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.RespositoryTest
{
    public class CustomerOrderRepositoryTest
    {
        CoffeeStoreContext context;
        IRepository<int, CustomerOrder> repository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);
            repository = new CustomerOrderRepository(context);
        }

        [Test, Order(1)]
        public async Task AddSuccessTest()
        {
            CustomerOrder customerOrder = new CustomerOrder()
            {
                CustomerId = 1,
                OrderId = 1
            };

            var newCustomerOrder = await repository.Add(customerOrder);
            Assert.AreEqual(1, newCustomerOrder.Id);
        }

        [Test, Order(2)]
        public async Task AddFailureTest()
        {
            CustomerOrder customerOrder = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Add(customerOrder));
        }

        [Test, Order(3)]
        public async Task DeleteSuccessTest()
        {
            CustomerOrder customerOrder = new CustomerOrder()
            {
                CustomerId = 2,
                OrderId = 2
            };

            var newCustomerOrder = await repository.Add(customerOrder);
            var deletedCustomerOrder = await repository.Delete(newCustomerOrder.Id);
            Assert.AreEqual(newCustomerOrder.Id, deletedCustomerOrder.Id);
        }

        [Test, Order(4)]
        public async Task DeleteFailureTest()
        {
            Assert.ThrowsAsync<NoSuchCustomerOrderException>(async () => await repository.Delete(100));
        }

        [Test, Order(5)]
        public async Task GetAllSuccessTest()
        {
            CustomerOrder customerOrder = new CustomerOrder() { CustomerId = 1, OrderId = 1 };
            
            await repository.Add(customerOrder);

            var customerOrders = await repository.GetAll();
            Assert.AreEqual(2, customerOrders.Count());
        }

        [Test, Order(6)]
        public async Task GetAllFailureTest()
        {
            foreach (var customerOrder in context.CustomerOrders)
            {
                context.CustomerOrders.Remove(customerOrder);
            }
            await context.SaveChangesAsync();

            Assert.ThrowsAsync<NoCustomerOrdersFoundException>(async () => await repository.GetAll());
        }

        [Test, Order(7)]
        public async Task GetByIdSuccessTest()
        {
            CustomerOrder customerOrder = new CustomerOrder() { CustomerId = 1, OrderId = 1 };
            var newCustomerOrder = await repository.Add(customerOrder);

            var retrievedCustomerOrder = await repository.GetById(newCustomerOrder.Id);
            Assert.AreEqual(newCustomerOrder.Id, retrievedCustomerOrder.Id);
        }

        [Test, Order(8)]
        public async Task GetByIdFailureTest()
        {
            Assert.ThrowsAsync<NoSuchCustomerOrderException>(async () => await repository.GetById(100));
        }

        [Test, Order(9)]
        public async Task UpdateSuccessTest()
        {
            CustomerOrder customerOrder = new CustomerOrder() { CustomerId = 1, OrderId = 1 };
            var newCustomerOrder = await repository.Add(customerOrder);
            newCustomerOrder.OrderId = 2;

            var updatedCustomerOrder = await repository.Update(newCustomerOrder);
            Assert.AreEqual(2, updatedCustomerOrder.OrderId);
        }

        [Test, Order(10)]
        public async Task UpdateFailureTest()
        {
            CustomerOrder customerOrder = new CustomerOrder() { CustomerId = 1, OrderId = 1 };
            var newCustomerOrder = await repository.Add(customerOrder);
            newCustomerOrder.Id = 100;

            Assert.ThrowsAsync<NoSuchCustomerOrderException>(async () => await repository.Update(newCustomerOrder));
        }
    }
}

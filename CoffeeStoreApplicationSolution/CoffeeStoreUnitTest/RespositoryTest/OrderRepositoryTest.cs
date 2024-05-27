using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.OrderExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.RespositoryTest
{
    public class OrderRepositoryTest
    {
        CoffeeStoreContext context;
        IRepository<int, Order> repository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);
            repository = new OrderRepository(context);
        }

        [Test, Order(1)]
        public async Task AddSuccessTest()
        {
            Order order = new Order()
            {
                CustomerId = 1,
                Status = OrderStatus.Placed,
                TotalPrice = 50,
                UseLoyaltyPoints = false,
                OrderItems = null,
                CustomerOrder = null
            };

            var newOrder = await repository.Add(order);
            Assert.AreEqual(1, newOrder.Id);
        }

        [Test, Order(2)]
        public async Task AddFailureTest()
        {
            Order order = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Add(order));
        }

        [Test, Order(3)]
        public async Task DeleteSuccessTest()
        {
            Order order = new Order()
            {
                CustomerId = 1,
                Status = OrderStatus.Placed,
                TotalPrice = 50,
                UseLoyaltyPoints = false,
                OrderItems = null,
                CustomerOrder = null
            };

            var newOrder = await repository.Add(order);
            var deletedOrder = await repository.Delete(newOrder.Id);
            Assert.AreEqual(newOrder.Id, deletedOrder.Id);
        }

        [Test, Order(4)]
        public async Task DeleteFailureTest()
        {
            Assert.ThrowsAsync<NoSuchOrderException>(async () => await repository.Delete(100));
        }

        [Test, Order(5)]
        public async Task GetAllSuccessTest()
        {
            Order order1 = new Order()
            {
                CustomerId = 1,
                Status = OrderStatus.Placed,
                TotalPrice = 50,
                UseLoyaltyPoints = false,
                OrderItems = null,
                CustomerOrder = null
            };

            Order order2 = new Order()
            {
                CustomerId = 2,
                Status = OrderStatus.Completed,
                TotalPrice = 100,
                UseLoyaltyPoints = true,
                OrderItems = null,
                CustomerOrder = null
            };

            await repository.Add(order1);
            await repository.Add(order2);

            var orders = await repository.GetAll();
            Assert.AreEqual(3, orders.Count());
        }

        [Test, Order(6)]
        public async Task GetAllFailureTest()
        {
            foreach (var order in context.Orders)
            {
                context.Orders.Remove(order);
            }
            await context.SaveChangesAsync();
            Assert.ThrowsAsync<NoOrdersFoundException>(async () => await repository.GetAll());
        }

        [Test, Order(7)]
        public async Task GetByIdSuccessTest()
        {
            Order order = new Order()
            {
                CustomerId = 1,
                Status = OrderStatus.Preparing,
                TotalPrice = 50,
                UseLoyaltyPoints = false,
                OrderItems = null,
                CustomerOrder = null
            };

            var newOrder = await repository.Add(order);

            var retrievedOrder = await repository.GetById(newOrder.Id);
            Assert.AreEqual(newOrder.Id, retrievedOrder.Id);
        }

        [Test, Order(8)]
        public async Task GetByIdFailureTest()
        {
            Assert.ThrowsAsync<NoSuchOrderException>(async () => await repository.GetById(100));
        }

        [Test, Order(9)]
        public async Task UpdateSuccessTest()
        {
            Order order = new Order()
            {
                CustomerId = 1,
                Status = OrderStatus.Placed,
                TotalPrice = 50,
                UseLoyaltyPoints = false,
                OrderItems = null,
                CustomerOrder = null
            };

            var newOrder = await repository.Add(order);
            newOrder.Status = OrderStatus.Completed;

            var updatedOrder = await repository.Update(newOrder);
            Assert.AreEqual(OrderStatus.Completed, updatedOrder.Status);
        }

        [Test, Order(10)]
        public async Task UpdateFailureTest()
        {
            Order order = new Order()
            {
                CustomerId = 1,
                Status = OrderStatus.Placed,
                TotalPrice = 50,
                UseLoyaltyPoints = false,
                OrderItems = null,
                CustomerOrder = null
            };

            var newOrder = await repository.Add(order);
            newOrder.Id = 100;

            Assert.ThrowsAsync<NoSuchOrderException>(async () => await repository.Update(newOrder));
        }
    }
}

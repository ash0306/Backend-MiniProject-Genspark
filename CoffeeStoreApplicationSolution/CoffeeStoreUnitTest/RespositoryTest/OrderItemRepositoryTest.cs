using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.OrderItemExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.RespositoryTest
{
    public class OrderItemRepositoryTest
    {
        CoffeeStoreContext context;
        IRepository<int, OrderItem> repository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);
            repository = new OrderItemRepository(context);
        }

        [Test, Order(1)]
        public async Task AddSuccessTest()
        {
            OrderItem orderItem = new OrderItem()
            {
                OrderId = 1,
                ProductId = 1
            };

            var newOrderItem = await repository.Add(orderItem);
            Assert.AreEqual(1, newOrderItem.Id);
        }

        [Test, Order(2)]
        public async Task AddFailureTest()
        {
            OrderItem orderItem = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Add(orderItem));
        }

        [Test, Order(3)]
        public async Task DeleteSuccessTest()
        {
            OrderItem orderItem = new OrderItem()
            {
                OrderId = 1,
                ProductId = 1
            };

            var newOrderItem = await repository.Add(orderItem);
            var deletedOrderItem = await repository.Delete(newOrderItem.Id);
            Assert.AreEqual(newOrderItem.Id, deletedOrderItem.Id);
        }

        [Test, Order(4)]
        public async Task DeleteFailureTest()
        {
            Assert.ThrowsAsync<NoSuchOrderItemException>(async () => await repository.Delete(100));
        }

        [Test, Order(5)]
        public async Task GetAllSuccessTest()
        {
            OrderItem orderItem1 = new OrderItem()
            {
                OrderId = 1,
                ProductId = 1
            };

            OrderItem orderItem2 = new OrderItem()
            {
                OrderId = 2,
                ProductId = 2
            };

            await repository.Add(orderItem1);
            await repository.Add(orderItem2);

            var orderItems = await repository.GetAll();
            Assert.AreEqual(3, orderItems.Count());
        }

        [Test, Order(6)]
        public async Task GetAllFailureTest()
        {
            foreach (var orderItem in context.OrderItems)
            {
                context.OrderItems.Remove(orderItem);
            }
            await context.SaveChangesAsync();
            Assert.ThrowsAsync<NoOrderItemsFoundException>(async () => await repository.GetAll());
        }

        [Test, Order(7)]
        public async Task GetByIdSuccessTest()
        {
            OrderItem orderItem = new OrderItem()
            {
                OrderId = 1,
                ProductId = 1
            };

            var newOrderItem = await repository.Add(orderItem);

            var retrievedOrderItem = await repository.GetById(newOrderItem.Id);
            Assert.AreEqual(newOrderItem.Id, retrievedOrderItem.Id);
        }

        [Test, Order(8)]
        public async Task GetByIdFailureTest()
        {
            Assert.ThrowsAsync<NoSuchOrderItemException>(async () => await repository.GetById(100));
        }

        [Test, Order(9)]
        public async Task UpdateSuccessTest()
        {
            OrderItem orderItem = new OrderItem()
            {
                OrderId = 1,
                ProductId = 1
            };

            var newOrderItem = await repository.Add(orderItem);
            newOrderItem.ProductId = 2;

            var updatedOrderItem = await repository.Update(newOrderItem);
            Assert.AreEqual(2, updatedOrderItem.ProductId);
        }

        [Test, Order(10)]
        public async Task UpdateFailureTest()
        {
            OrderItem orderItem = new OrderItem()
            {
                OrderId = 1,
                ProductId = 1
            };

            var newOrderItem = await repository.Add(orderItem);
            newOrderItem.Id = 100;

            Assert.ThrowsAsync<NoSuchOrderItemException>(async () => await repository.Update(newOrderItem));
        }
    }
}

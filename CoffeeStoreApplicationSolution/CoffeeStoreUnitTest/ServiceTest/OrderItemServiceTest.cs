using AutoMapper;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.OrderItems;
using CoffeeStoreApplication.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.ServiceTest
{
    public class OrderItemServiceTest
    {
        private Mock<IRepository<int, OrderItem>> _mockOrderItemRepository;
        private Mock<IRepository<int, Product>> _mockProductRepository;
        private IMapper _mapper;
        private OrderItemService _orderItemService;
        private Mock<ILogger<OrderItemService>> _logger;

        [SetUp]
        public void Setup()
        {
            _mockOrderItemRepository = new Mock<IRepository<int, OrderItem>>();
            _mockProductRepository = new Mock<IRepository<int, Product>>();

            var config = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "CoffeeStoreApplication" }));

            _mapper = config.CreateMapper();
            _logger = new Mock<ILogger<OrderItemService>>();

            _orderItemService = new OrderItemService(
                _mockOrderItemRepository.Object,
                _mapper,
                _mockProductRepository.Object, _logger.Object);
        }

        [Test]
        public async Task GetOrderItemsByOrderId_Success()
        {
            int orderId = 1;

            var orderItems = new List<OrderItem>
            {
                new OrderItem { Id = 1, OrderId = orderId, ProductId = 1},
                new OrderItem {Id = 2, OrderId = orderId, ProductId = 2}
            };

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Coffee" },
                new Product { Id = 2, Name = "Tea" }
            };

            _mockOrderItemRepository.Setup(repo => repo.GetAll()).ReturnsAsync(orderItems);
            _mockProductRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((int id) => products.FirstOrDefault(p => p.Id == id));

            var result = await _orderItemService.GetOrderItemsByOrderId(orderId);

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetOrderItemsByOrderIdFailureTest()
        {
            int orderId = 1;

            _mockOrderItemRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<OrderItem>());

            Assert.ThrowsAsync<NoItemsFoundException>(async () => await _orderItemService.GetOrderItemsByOrderId(orderId));
        }
    }
}

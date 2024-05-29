using AutoMapper;
using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.CustomerOrderExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.CustomerOrder;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Repositories;
using CoffeeStoreApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.ServiceTest
{
    public class CustomerOrderServiceTest
    {
        private CoffeeStoreContext _context;
        private IRepository<int, CustomerOrder> _customerOrderRepository;
        private IRepository<int, Order> _orderRepository;
        private IRepository<int, Customer> _customerRepository;
        private IOrderItemService _orderItemService;
        private ICustomerOrderService _customerOrderService;
        private IMapper _mapper;
        private MapperConfiguration _mapperConfig;
        private Mock<ILogger<CustomerOrderService>> _logger;
        private Mock<ILogger<OrderItemService>> _orderItemLogger;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<CoffeeStoreContext>().UseInMemoryDatabase("dummyCustomerOrdersDB").Options;
            _context = new CoffeeStoreContext(options);

            _mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "CoffeeStoreApplication" }));
            _mapper = _mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<CustomerOrderService>>();
            _orderItemLogger = new Mock<ILogger<OrderItemService>>();

            _customerOrderRepository = new CustomerOrderRepository(_context);
            _orderRepository = new OrderRepository(_context);
            _customerRepository = new CustomerRepository(_context);
            _orderItemService = new OrderItemService(new OrderItemRepository(_context), _mapper, new ProductRepository(_context), _orderItemLogger.Object);
            _customerOrderService = new CustomerOrderService(_customerOrderRepository, _mapper, _orderRepository, _customerRepository, _orderItemService, _logger.Object);

            await SeedDatabase();
        }

        private async Task SeedDatabase()
        {
            // Clear existing data to avoid key conflicts
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var hmac = new HMACSHA512();
            Customer customer1 = new Customer()
            {
                Id = 1,
                Name = "Test",
                DateOfBirth = DateTime.Now,
                Email = "test1@gamil.com",
                Phone = "1234566789",
                LoyaltyPoints = 0,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("testing123"))
            };

            var order = new Order
            {
                Id = 1,
                Status = OrderStatus.Placed,
                TotalPrice = 200,
                CustomerId = 1,
                LoyaltyPointsDiscount = 0
            };

            var customerOrder = new CustomerOrder
            {
                Id = 1,
                CustomerId = 1,
                OrderId = 1
            };

            var product = new Product
            {
                Id = 1,
                Name = "Latte",
                Category = ProductCategory.HotDrinks,
                Description = "",
                Price = 100,
                Status = ProductStatus.Available,
                Stock = 10
            };

            var orderItem = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1
            };

            _context.Customers.Add(customer1);
            _context.Products.Add(product);
            _context.Orders.Add(order);
            _context.OrderItems.Add(orderItem);
            _context.CustomerOrders.Add(customerOrder);

            await _context.SaveChangesAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            _context.Database.EnsureDeleted();
            await _context.Database.EnsureCreatedAsync();
        }

        [Test, Order(1)]
        public async Task GetCustomerOrderById_Success()
        {
            var result = await _customerOrderService.GetCustomerOrderById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test, Order(2)]
        public void GetCustomerOrderById_Failure()
        {
            foreach (var customerOrder in _context.CustomerOrders)
            {
                _context.CustomerOrders.Remove(customerOrder);
            }
            _context.SaveChanges();
            Assert.ThrowsAsync<NoCustomerOrdersFoundException>(async () => await _customerOrderService.GetCustomerOrderById(99));
        }
    }
}

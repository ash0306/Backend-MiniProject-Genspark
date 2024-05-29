using AutoMapper;
using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Exceptions.OrderExceptions;
using CoffeeStoreApplication.Exceptions.OrderItemExceptions;
using CoffeeStoreApplication.Exceptions.ProductExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Order;
using CoffeeStoreApplication.Models.DTOs.OrderItems;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Repositories;
using CoffeeStoreApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Cryptography;
using System.Text;

namespace CoffeeStoreUnitTest.ServiceTest
{
    public class OrderServiceTest
    {
        CoffeeStoreContext context;
        IRepository<int, Order> orderRepository;
        IRepository<int, Product> productRepository;
        IRepository<int, Customer> customerRepository;
        IRepository<int, OrderItem> orderItemRepository;
        IRepository<int, CustomerOrder> customerOrderRepository;
        IOrderService orderService;
        IOrderItemService orderItemService;
        ICustomerService customerService;
        IMapper mapper;
        MapperConfiguration mapperConfig;
        Mock<ILogger<OrderService>> logger;
        Mock<ILogger<CustomerService>> customerLogger;
        Mock<ILogger<OrderItemService>> orderItemLogger;

        [SetUp]
        public async Task Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("dummyOrderDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);

            mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "CoffeeStoreApplication" }));
            mapper = mapperConfig.CreateMapper();
            logger = new Mock<ILogger<OrderService>>();
            orderItemLogger = new Mock<ILogger<OrderItemService>>();
            customerLogger = new Mock<ILogger<CustomerService>>();

            orderRepository = new OrderRepository(context);
            productRepository = new ProductRepository(context);
            customerRepository = new CustomerRepository(context);
            orderItemRepository = new OrderItemRepository(context);
            customerOrderRepository = new CustomerOrderRepository(context);
            orderItemService = new OrderItemService(orderItemRepository, mapper, productRepository, orderItemLogger.Object);
            customerService = new CustomerService(customerRepository, mapper, customerLogger.Object);

            orderService = new OrderService(orderRepository, mapper, productRepository, customerRepository, orderItemRepository, customerOrderRepository, logger.Object, orderItemService, customerService);

            #region SeedDatabase
            var hmac = new HMACSHA512();
            var product1 = new Product()
            {
                Name = "Latte",
                Price = 100,
                Stock = 10,
                Category = ProductCategory.HotDrinks,
                Description = "Testing",
                Status = ProductStatus.Available
            };
            var product2 = new Product()
            {
                Name = "Espresso",
                Price = 100,
                Stock = 10,
                Category = ProductCategory.HotDrinks,
                Description = "Testing",
                Status = ProductStatus.Available
            };
            var product3 = new Product()
            {
                Name = "Americano",
                Price = 100,
                Stock = 10,
                Category = ProductCategory.HotDrinks,
                Description = "Testing",
                Status = ProductStatus.Available
            };

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

            await productRepository.Add(product1);
            await productRepository.Add(product2);
            await productRepository.Add(product3);
            await customerRepository.Add(customer1);
            await customerRepository.Add(customer2);
            #endregion
        }

        [Test, Order(1)]
        public async Task AddOrderSuccessTest()
        {
            var orderDTO = new OrderDTO()
            {
                CustomerId = 1,
                UseLoyaltyPoints = false,
                OrderItems = new List<OrderItemDTO>()
                {
                    new OrderItemDTO() { ProductName = "Latte" },
                    new OrderItemDTO() { ProductName = "Espresso" }
                }
            };

            var result = await orderService.AddOrder(orderDTO);

            Assert.AreEqual(1, result.CustomerId);
            Assert.AreEqual(2, result.OrderItems.Count());
        }

        [Test, Order(2)]
        public async Task AddOrderFailureTest()
        {
            var orderDTO = new OrderDTO()
            {
                CustomerId = 1,
                UseLoyaltyPoints = false,
                OrderItems = new List<OrderItemDTO>()
            };

            Assert.ThrowsAsync<NoItemsFoundException>(async () => await orderService.AddOrder(orderDTO));
        }

        [Test, Order(3)]
        public async Task CancelOrderSuccessTest()
        {
            var orderDTO = new OrderDTO()
            {
                CustomerId = 1,
                UseLoyaltyPoints = false,
                OrderItems = new List<OrderItemDTO>()
                {
                    new OrderItemDTO() { ProductName = "Latte" }
                }
            };

            var addedOrder = await orderService.AddOrder(orderDTO);

            var result = await orderService.CancelOrder(addedOrder.Id);

            Assert.AreEqual(OrderStatus.Cancelled.ToString(), result.Status);
        }

        [Test, Order(4)]
        public async Task CancelOrderAlreadyCancelledTest()
        {
            var orderDTO = new OrderDTO()
            {
                CustomerId = 1,
                UseLoyaltyPoints = false,
                OrderItems = new List<OrderItemDTO>()
                {
                    new OrderItemDTO() { ProductName = "Latte" }
                }
            };

            var addedOrder = await orderService.AddOrder(orderDTO);
            await orderService.CancelOrder(addedOrder.Id);

            Assert.ThrowsAsync<NoSuchOrderException>(async () => await orderService.CancelOrder(addedOrder.Id));
        }

        [Test, Order(5)]
        public async Task GetAllPendingOrdersSuccessTest()
        {
            var orderDTO1 = new OrderDTO()
            {
                CustomerId = 1,
                UseLoyaltyPoints = false,
                OrderItems = new List<OrderItemDTO>()
                {
                    new OrderItemDTO() { ProductName = "Latte" }
                }
            };

            var orderDTO2 = new OrderDTO()
            {
                CustomerId = 2,
                UseLoyaltyPoints = false,
                OrderItems = new List<OrderItemDTO>()
                {
                    new OrderItemDTO() { ProductName = "Espresso" }
                }
            };

            await orderService.AddOrder(orderDTO1);
            await orderService.AddOrder(orderDTO2);

            var result = await orderService.GetAllPendingOrders();

            Assert.AreEqual(3, result.Count());
        }

        [Test, Order(6)]
        public async Task UpdateOrderStatusSuccessTest()
        {
            var orderDTO = new OrderDTO()
            {
                CustomerId = 1,
                UseLoyaltyPoints = false,
                OrderItems = new List<OrderItemDTO>()
                {
                    new OrderItemDTO() { ProductName = "Latte" }
                }
            };

            var addedOrder = await orderService.AddOrder(orderDTO);

            var orderStatusDTO = new OrderStatusDTO()
            {
                OrderId = addedOrder.Id,
                Status = "Completed"
            };

            var result = await orderService.UpdateOrderStatus(orderStatusDTO);

            Assert.AreEqual("Completed", result.Status);
        }

        [Test, Order(7)]
        public async Task UpdateOrderStatusFailureTest()
        {
            var orderStatusDTO = new OrderStatusDTO()
            {
                OrderId = 99,
                Status = "Completed"
            };

            Assert.ThrowsAsync<NoSuchOrderException>(async () => await orderService.UpdateOrderStatus(orderStatusDTO));
        }

        [Test, Order(8)]
        public async Task GetAllPendingOrdersFailureTest()
        {
            foreach (var order in context.Orders)
            {
                context.Orders.Remove(order);
            }
            await context.SaveChangesAsync();
            Assert.ThrowsAsync<NoOrdersFoundException>(async () => await orderService.GetAllPendingOrders());
        }
    }
}

using AutoMapper;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Exceptions.OrderExceptions;
using CoffeeStoreApplication.Exceptions.OrderItemExceptions;
using CoffeeStoreApplication.Exceptions.ProductExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.DTOs.Order;
using CoffeeStoreApplication.Models.Enum;
using System.Diagnostics.CodeAnalysis;

namespace CoffeeStoreApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<int, Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<int, Product> _productRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IRepository<int, OrderItem> _orderItemRepository;
        private readonly IRepository<int, CustomerOrder> _customerOrderRepository;
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderItemService _orderItemService;
        private readonly ICustomerService _customerService;

        public OrderService(IRepository<int, Order> orderRepo, IMapper mapper, 
            IRepository<int, Product> productRepo, IRepository<int,Customer> customerRepo, 
            IRepository<int, OrderItem> orderItemRepo,IRepository<int,CustomerOrder> customerOrderRepo,
            ILogger<OrderService> logger, IOrderItemService orderItemService,
            ICustomerService customerService)
        {
            _orderRepository = orderRepo;
            _mapper = mapper;
            _productRepository = productRepo;
            _customerRepository = customerRepo;
            _orderItemRepository = orderItemRepo;
            _customerOrderRepository = customerOrderRepo;
            _logger = logger;
            _orderItemService = orderItemService;
            _customerService = customerService;
        }

        /// <summary>
        /// Adds a new order.
        /// </summary>
        /// <param name="orderDTO">OrderDTO object containing order details</param>
        /// <returns>OrderReturnDTO object containing added order details</returns>
        /// <exception cref="NoItemsFoundException">If no items are found in the order</exception>
        public async Task<OrderReturnDTO> AddOrder(OrderDTO orderDTO)
        {
            Order order;
            float totalPrice = 0;
            float loyaltyDiscount = 0;
            IList<OrderItem> orderItems = new List<OrderItem>();
            if(orderDTO.OrderItems.Count() == 0) {
                _logger.LogError("No items in order");
                throw new NoItemsFoundException("This order has no items!! Add atleast 1 item to place an order.");
            }

            totalPrice = await CalculateTotalPrice(orderDTO);

            if(orderDTO.UseLoyaltyPoints)
            {
                loyaltyDiscount = await CalculateLoyaltyDiscount(orderDTO, totalPrice);
                totalPrice -= loyaltyDiscount;
            }
            else
            {
                var customer = await _customerService.GetCustomerById(orderDTO.CustomerId);
                var loyaltyPoints = (int)(totalPrice / 10);
                await _customerService.UpdateLoyaltyPoints(new LoyaltyPointsDTO() { Email =  customer.Email, LoyaltyPoints = loyaltyPoints});
            }

            order = new Order()
            {
                CustomerId = orderDTO.CustomerId,
                LoyaltyPointsDiscount = loyaltyDiscount,
                TotalPrice = totalPrice,
                UseLoyaltyPoints = orderDTO.UseLoyaltyPoints,
                Status = OrderStatus.Placed
            };

            var result = await _orderRepository.Add(order);

            orderItems = (await AddOrderItems(orderDTO, result.Id)).ToList();
            order.OrderItems = orderItems;
            result = await _orderRepository.Update(order);

            CustomerOrder customerOrder = new CustomerOrder()
            {
                CustomerId = orderDTO.CustomerId,
                OrderId = result.Id
            };
            await _customerOrderRepository.Add(customerOrder);

            OrderReturnDTO orderReturnDTO = _mapper.Map<OrderReturnDTO>(result);
            return orderReturnDTO;
        }

        /// <summary>
        /// Calculates the total price of the order.
        /// </summary>
        /// <param name="orderDTO">OrderDTO object containing order details</param>
        /// <returns>Total price of the order</returns>
        /// <exception cref="NoSuchProductException">If a product in the order is not found</exception>
        /// <exception cref="ProductOutOfStockException">If a product in the order is out of stock</exception>
        [ExcludeFromCodeCoverage]
        public async Task<float> CalculateTotalPrice(OrderDTO orderDTO)
        {
            float totalPrice = 0;   
            foreach (var item in orderDTO.OrderItems)
            {
                var product = (await _productRepository.GetAll()).FirstOrDefault(p => p.Name == item.ProductName);
                if (product == null)
                    throw new NoSuchProductException("No product with the given name found");
                if (product.Stock < 1)
                    throw new ProductOutOfStockException($"The product {product.Name} isnt available");
                totalPrice += product.Price;
            }
            return totalPrice;
        }

        /// <summary>
        /// Calculates the loyalty discount for the order.
        /// </summary>
        /// <param name="orderDTO">OrderDTO object containing order details</param>
        /// <param name="totalPrice">Total price of the order</param>
        /// <returns>Loyalty discount for the order</returns>
        /// <exception cref="InsufficientPointsException">If the customer has insufficient loyalty points</exception>
        [ExcludeFromCodeCoverage]
        public async Task<float> CalculateLoyaltyDiscount(OrderDTO orderDTO, float totalPrice)
        {
            float loyaltyDiscount = 0;
            var customer = (await _customerRepository.GetAll()).FirstOrDefault(c => c.Id == orderDTO.CustomerId);
            if (customer.LoyaltyPoints < 100)
                throw new InsufficientPointsException("You have less than 100 points. Need minimum 100 points to deduct discount");

            if(customer.LoyaltyPoints > totalPrice)
            {
                loyaltyDiscount = totalPrice;
                return loyaltyDiscount;
            }
            loyaltyDiscount = customer.LoyaltyPoints;

            return loyaltyDiscount;
        }

        /// <summary>
        /// Adds order items to the order.
        /// </summary>
        /// <param name="orderDTO">OrderDTO object containing order details</param>
        /// <param name="orderId">ID of the order</param>
        /// <returns>List of added OrderItem objects</returns>
        /// <exception cref="UnableToUpdateProductException">If unable to update the product stock</exception>
        /// <exception cref="UnableToAddOrderItemException">If unable to add the order item</exception>
        [ExcludeFromCodeCoverage]
        public async Task<IEnumerable<OrderItem>> AddOrderItems(OrderDTO orderDTO, int orderId)
        {
            IList<OrderItem> orderItems = new List<OrderItem>();

            foreach (var item in orderDTO.OrderItems)
            {
                var product = (await _productRepository.GetAll()).FirstOrDefault(p => p.Name == item.ProductName);

                OrderItem orderItem = new OrderItem()
                {
                    OrderId = orderId,
                    ProductId = product.Id
                };

                orderItems.Add(orderItem);

                product.Stock -= 1;
                if(product.Stock <= 0)
                {
                    product.Status = ProductStatus.Unavailable;
                }
                var updatedProduct = await _productRepository.Update(product);
                if(updatedProduct == null)
                {
                    throw new UnableToUpdateProductException("Could not update product stock at the moment");
                }

                var itemOrder = await _orderItemRepository.Add(orderItem);
                if (itemOrder == null)
                {
                    throw new UnableToAddOrderItemException("Unable to add order item");
                }
            }
            return orderItems;
        }

        /// <summary>
        /// Cancels an order.
        /// </summary>
        /// <param name="orderId">ID of the order to be cancelled</param>
        /// <returns>OrderReturnDTO object containing cancelled order details</returns>
        /// <exception cref="NoSuchOrderException">If the order is not found or is already cancelled</exception>
        /// <exception cref="UnableToCancelOrderException">If the order cannot be cancelled</exception>
        public async Task<OrderReturnDTO> CancelOrder(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if(order == null)
            {
                _logger.LogError("No order found");
                throw new NoSuchOrderException($"No order found with ID {orderId}");
            }

            if (order.Status == OrderStatus.Cancelled)
                throw new NoSuchOrderException("Your order has already been cancelled");

            if(order.Status != OrderStatus.Placed)
            {
                throw new UnableToCancelOrderException("Your order is already being prepared so cannot cancel order");
            }
            var items = (await _orderItemRepository.GetAll()).Where(oi => oi.OrderId == order.Id);
            var customer = await _customerRepository.GetById(order.CustomerId);
            customer.LoyaltyPoints += (int)order.LoyaltyPointsDiscount;
            await _customerRepository.Update(customer);
            foreach (var item in items)
            {
                var product = await _productRepository.GetById(item.ProductId);
                product.Stock += 1;
                if(product.Stock == 1)
                {
                    product.Status = ProductStatus.Available;
                }
                await _productRepository.Update(product);
            }
            order.Status = OrderStatus.Cancelled;
            var result = await _orderRepository.Update(order);
            OrderReturnDTO orderReturnDTO = _mapper.Map<OrderReturnDTO>(result);

            return orderReturnDTO;
        }


        /// <summary>
        /// Gets all pending orders.
        /// </summary>
        /// <returns>List of OrderReturnDTO objects containing pending order details</returns>
        /// <exception cref="NoSuchOrderException">If no pending orders are found</exception>
        public async Task<IEnumerable<OrderReturnDTO>> GetAllPendingOrders()
        {
            var orders = (await _orderRepository.GetAll()).Where(o=> o.Status != OrderStatus.Completed && o.Status != OrderStatus.Cancelled);

            if (orders.Count() == 0)
            {
                _logger.LogError("No orders found");
                throw new NoSuchOrderException("No pending orders found.");
            }
            IList<OrderReturnDTO> orderReturnDTOs = new List<OrderReturnDTO>();
            foreach (var item in orders)
            {
                var orderitems = (await _orderItemService.GetOrderItemsByOrderId(item.Id)).ToList();
                OrderReturnDTO returnDTO = _mapper.Map<OrderReturnDTO>(item);
                returnDTO.OrderItems = orderitems;
                orderReturnDTOs.Add(returnDTO);
            }
            return orderReturnDTOs;
        }


        /// <summary>
        /// Updates the status of an order.
        /// </summary>
        /// <param name="orderStatusDTO">OrderStatusDTO object containing the order ID and new status</param>
        /// <returns>OrderStatusDTO object containing the updated order status</returns>
        /// <exception cref="NoSuchOrderException">If the order is not found</exception>
        /// <exception cref="UnableToUpdateOrderException">If unable to update the order status</exception>
        public async Task<OrderStatusDTO> UpdateOrderStatus(OrderStatusDTO orderStatusDTO)
        {
            var order = await _orderRepository.GetById(orderStatusDTO.OrderId);
            if (order == null)
            {
                _logger.LogError("No order found");
                throw new NoSuchOrderException($"No order found with ID {orderStatusDTO.OrderId}");
            }
            order.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatusDTO.Status);

            var updatedOrder = await _orderRepository.Update(order);
            if (updatedOrder == null)
                throw new UnableToUpdateOrderException("Unable to update the order status at this moment");
            
            return orderStatusDTO;
        }
    }
}

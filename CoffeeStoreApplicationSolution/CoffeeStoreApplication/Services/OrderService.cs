using AutoMapper;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Exceptions.OrderExceptions;
using CoffeeStoreApplication.Exceptions.ProductExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Order;
using CoffeeStoreApplication.Models.Enum;

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

        public OrderService(IRepository<int, Order> orderRepo, 
            IMapper mapper, 
            IRepository<int, Product> productRepo, 
            IRepository<int,Customer> customerRepo, 
            IRepository<int, OrderItem> orderItemRepo,
            IRepository<int,CustomerOrder> customerOrderRepo)
        {
            _orderRepository = orderRepo;
            _mapper = mapper;
            _productRepository = productRepo;
            _customerRepository = customerRepo;
            _orderItemRepository = orderItemRepo;
            _customerOrderRepository = customerOrderRepo;
        }

        public async Task<OrderReturnDTO> AddOrder(OrderDTO orderDTO)
        {
            Order order;
            float totalPrice = 0;
            float loyaltyDiscount = 0;

            if(orderDTO.OrderItems.Count() == 0)
            {
                throw new NoItemsFoundException("This order has no items!! Add atleast 1 item to place an order.");
            }

            foreach (var item in orderDTO.OrderItems)
            {
                var product = (await _productRepository.GetAll()).FirstOrDefault(p => p.Name == item.ProductName);
                if (product == null)
                    throw new NoSuchProductException("No product with the given name found");
                totalPrice += product.Price;
            }

            if(orderDTO.UseLoyaltyPoints)
            {
                var customer = (await _customerRepository.GetAll()).FirstOrDefault(c=>c.Id == orderDTO.CustomerId);
                if (customer.LoyaltyPoints < 100)
                    throw new InsufficientPointsException("You have less than 100 points. Need minimum 100 points to deduct discount");
                loyaltyDiscount = customer.LoyaltyPoints;
                totalPrice -= loyaltyDiscount;
            }
            else
            {
                var customer = (await _customerRepository.GetAll()).FirstOrDefault(c => c.Id == orderDTO.CustomerId);
                customer.LoyaltyPoints += (int)(totalPrice / 10);
                await _customerRepository.Update(customer);
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
            orderDTO = _mapper.Map<OrderDTO>(result);

            foreach (var item in orderDTO.OrderItems)
            {
                var product = (await _productRepository.GetAll()).FirstOrDefault(p => p.Name == item.ProductName);
                OrderItem orderItem = new OrderItem()
                {
                    OrderId = result.Id,
                    ProductId = product.Id
                };

                var itemOrder = await _orderItemRepository.Add(orderItem);
            }

            CustomerOrder customerOrder = new CustomerOrder()
            {
                CustomerId = orderDTO.CustomerId,
                OrderId = result.Id
            };
            await _customerOrderRepository.Add(customerOrder);

            OrderReturnDTO orderReturnDTO = _mapper.Map<OrderReturnDTO>(orderDTO);
            return orderReturnDTO;
        }

        public async Task<OrderReturnDTO> CancelOrder(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if(order == null)
            {
                throw new NoSuchOrderException($"No order found with ID {orderId}");
            }

            if(order.Status != OrderStatus.Placed)
            {
                throw new UnableToCancelOrderException("Your order is already being prepared so cannot cancel order");
            }

            order.Status = OrderStatus.Cancelled;
            var result = await _orderRepository.Update(order);
            OrderReturnDTO orderReturnDTO = _mapper.Map<OrderReturnDTO>(result);
            return orderReturnDTO;
        }

        public async Task<IEnumerable<OrderReturnDTO>> GetAllPendingOrders()
        {
            var orders = (await _orderRepository.GetAll()).Where(o=> o.Status != OrderStatus.Completed || o.Status != OrderStatus.Cancelled);

            if(orders.Count() == 0)
                throw new NoSuchOrderException("No pending orders found.");

            IList<OrderReturnDTO> orderReturnDTOs = new List<OrderReturnDTO>();
            foreach (var item in orders)
            {
                orderReturnDTOs.Add(_mapper.Map<OrderReturnDTO>(item));
            }
            return orderReturnDTOs;
        }

        public async Task<OrderStatusDTO> UpdateOrderStatus(OrderStatusDTO orderStatusDTO)
        {
            var order = await _orderRepository.GetById(orderStatusDTO.OrderId);
            if (order == null)
            {
                throw new NoSuchOrderException($"No order found with ID {orderStatusDTO.OrderId}");
            }
            order.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatusDTO.Status);

            var updatedOrder = await _orderRepository.Update(order);
            if (updatedOrder == null)
                throw new UnableToUpdateOrderException("Unable to update the order status at this moment");
            
            orderStatusDTO = _mapper.Map<OrderStatusDTO>(updatedOrder);
            return orderStatusDTO;
        }
    }
}

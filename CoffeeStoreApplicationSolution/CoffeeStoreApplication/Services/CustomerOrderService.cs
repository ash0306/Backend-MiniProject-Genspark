using AutoMapper;
using CoffeeStoreApplication.Exceptions.OrderExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.CustomerOrder;
using CoffeeStoreApplication.Models.DTOs.OrderItems;

namespace CoffeeStoreApplication.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IRepository<int, CustomerOrder> _repository;
        private readonly IMapper _mapper;
        private readonly IRepository<int, Order> _orderRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IOrderItemService _orderItemService;

        public CustomerOrderService(IRepository<int, CustomerOrder> repository, IMapper mapper, 
            IRepository<int, Order> orderRepo, 
            IRepository<int, Customer> customerRepo,
            IOrderItemService orderItemService)
        {
            _repository = repository;
            _mapper = mapper;
            _orderRepository = orderRepo;
            _customerRepository = customerRepo;
            _orderItemService = orderItemService;
        }

        /// <summary>
        /// Gets all the Orders and their details for a given customer
        /// </summary>
        /// <param name="customerId">ID of the customer whose Orders are to be fetched</param>
        /// <returns>List of customer orders</returns>
        /// <exception cref="NoOrdersFoundException">Thrown if no orders were found for a given user</exception>
        public async Task<IEnumerable<CustomerOrderReturnDTO>> GetCustomerOrderById(int customerId)
        {
            IList<CustomerOrderReturnDTO> customerOrders = new List<CustomerOrderReturnDTO>();
            var result = (await _repository.GetAll()).Where(co=>co.CustomerId == customerId).ToList();
            
            if(result.Count == 0)
            {
                throw new NoOrdersFoundException($"No orders for customer with ID {customerId} found");
            }
            var customer = await _customerRepository.GetById(customerId);
            foreach (var item in result)
            {
                var order = await _orderRepository.GetById(item.OrderId);
                IList<OrderItemDTO> orderItems = new List<OrderItemDTO>();
                orderItems = (await _orderItemService.GetOrderItemsByOrderId(item.OrderId)).ToList();
                
                CustomerOrderReturnDTO returnDTO = new CustomerOrderReturnDTO() 
                {
                    CustomerId = customerId,
                    CustomerName = customer.Name,
                    OrderId = order.Id,
                    OrderStatus = order.Status.ToString(),
                    TotalOrderPrice = order.TotalPrice,
                    OrderItems = orderItems
                };
                customerOrders.Add(returnDTO);
            }
            return customerOrders;
        }
    }
}

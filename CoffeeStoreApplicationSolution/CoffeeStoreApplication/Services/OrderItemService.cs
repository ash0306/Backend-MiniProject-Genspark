using AutoMapper;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.OrderItems;

namespace CoffeeStoreApplication.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<int, OrderItem> _repository;
        private readonly IMapper _mapper;
        private readonly IRepository<int, Product> _productRepository;

        public OrderItemService(IRepository<int, OrderItem> repository, IMapper mapper, IRepository<int, Product> productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderId(int orderId)
        {
            var items = (await  _repository.GetAll()).Where(oi=>oi.OrderId == orderId).ToList();
            if(items.Count() == 0)
            {
                throw new NoItemsFoundException("Could not find any items in this order!!");
            }
            IList<OrderItemDTO> orderItemDTO = new List<OrderItemDTO>();

            foreach (var item in items)
            {
                var prod = await _productRepository.GetById(item.ProductId);
                OrderItemDTO tempDTO = new OrderItemDTO()
                {
                    ProductName = prod.Name
                };
                orderItemDTO.Add(tempDTO);
            }

            return orderItemDTO;
        }
    }
}

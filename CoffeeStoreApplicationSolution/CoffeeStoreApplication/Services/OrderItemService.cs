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

        public OrderItemService(IRepository<int, OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
                orderItemDTO.Add(_mapper.Map<OrderItemDTO>(item));
            }

            return orderItemDTO;
        }
    }
}

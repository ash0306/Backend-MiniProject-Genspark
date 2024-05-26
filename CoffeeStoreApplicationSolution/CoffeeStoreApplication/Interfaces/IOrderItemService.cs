using CoffeeStoreApplication.Models.DTOs.OrderItems;

namespace CoffeeStoreApplication.Interfaces
{
    public interface IOrderItemService
    {
        public Task<IEnumerable<OrderItemDTO>> GetOrderItemsByOrderId(int orderId);
    }
}

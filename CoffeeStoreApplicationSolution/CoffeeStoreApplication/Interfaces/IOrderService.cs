using CoffeeStoreApplication.Models.DTOs.Order;

namespace CoffeeStoreApplication.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderReturnDTO> AddOrder(OrderDTO orderDTO);
        public Task<OrderStatusDTO> UpdateOrderStatus(OrderStatusDTO orderStatusDTO);
        public Task<OrderReturnDTO> CancelOrder(int orderId);
        public Task<IEnumerable<OrderReturnDTO>> GetAllPendingOrders();
    }
}

using CoffeeStoreApplication.Models.DTOs.OrderItems;

namespace CoffeeStoreApplication.Models.DTOs.CustomerOrder
{
    public class CustomerOrderReturnDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int OrderId { get; set; }
        public IList<OrderItemDTO> OrderItems { get; set; }
        public float TotalOrderPrice { get; set; }
        public string OrderStatus { get; set; }
    }
}

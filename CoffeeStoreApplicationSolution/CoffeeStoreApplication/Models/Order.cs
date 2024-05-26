using CoffeeStoreApplication.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeStoreApplication.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public float TotalPrice { get; set; }
        public bool UseLoyaltyPoints { get; set; }
        public float? LoyaltyPointsDiscount { get; set; } = 0;
        public IList<OrderItem> OrderItems { get; set; }
        public CustomerOrder CustomerOrder { get; set; }
    }
}

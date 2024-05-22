namespace CoffeeStoreApplication.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public float TotalPrice { get; set; }
        public bool UseLoyaltyPoints { get; set; }
        public float LoyaltyPointsDiscount { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }
}

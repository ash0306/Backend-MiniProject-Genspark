using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeStoreApplication.Models
{
    public class CustomerOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}

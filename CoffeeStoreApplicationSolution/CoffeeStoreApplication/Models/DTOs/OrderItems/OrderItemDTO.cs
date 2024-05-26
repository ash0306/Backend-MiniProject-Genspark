using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.OrderItems
{
    public class OrderItemDTO
    {
        [Required]
        public string ProductName { get; set; }
    }
}

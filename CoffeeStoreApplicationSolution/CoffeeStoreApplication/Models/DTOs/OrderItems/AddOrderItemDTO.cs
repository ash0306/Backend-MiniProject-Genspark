using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.OrderItems
{
    public class AddOrderItemDTO
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.CustomerOrder
{
    public class CustomerOrderDTO
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int OrderId { get; set; }
    }
}

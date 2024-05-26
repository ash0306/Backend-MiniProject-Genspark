using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Validations;
using System.ComponentModel.DataAnnotations;
using CoffeeStoreApplication.Models.DTOs.OrderItems;

namespace CoffeeStoreApplication.Models.DTOs.Order
{
    public class OrderDTO
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [EnumValidation(typeof(OrderStatus))]
        public OrderStatus Status { get; set; }

        [Required]
        public float TotalPrice { get; set; }

        [Required]
        public bool UseLoyaltyPoints { get; set; }

        [Required]
        [MinLength(1)]
        public IList<OrderItemDTO> OrderItems { get; set; }
    }
}

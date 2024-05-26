using CoffeeStoreApplication.Models.DTOs.OrderItems;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Validations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CoffeeStoreApplication.Models.DTOs.Order
{
    public class OrderReturnDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [EnumValidation(typeof(OrderStatus))]
        public string Status { get; set; }

        [Required]
        public float TotalPrice { get; set; }

        [Required]
        public bool UseLoyaltyPoints { get; set; }
        public float? LoyaltyPointsDiscount { get; set; } = 0;

        [Required]
        public IList<OrderItemDTO> OrderItems { get; set; }
    }
}

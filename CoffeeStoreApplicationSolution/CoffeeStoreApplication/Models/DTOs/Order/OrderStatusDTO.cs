using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Validations;
using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Order
{
    public class OrderStatusDTO
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        [EnumValidation(typeof(OrderStatus))]
        public string Status { get; set; }
    }
}

using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Validations;
using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Product
{
    public class ProductStatusDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EnumValidation(typeof(ProductStatus))]
        public string Status { get; set; }
    }
}

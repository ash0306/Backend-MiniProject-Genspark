using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Product
{
    public class ProductStockDTO
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Stock {  get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Models.DTOs.Product
{
    public class ProductStockDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public int Stock {  get; set; }
    }
}

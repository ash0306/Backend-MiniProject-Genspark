using CoffeeStoreApplication.Models.Enum;

namespace CoffeeStoreApplication.Models.DTOs.Product
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}

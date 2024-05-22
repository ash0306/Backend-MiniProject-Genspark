using CoffeeStoreApplication.Models.Enum;

namespace CoffeeStoreApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public ProductStatus Status { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}

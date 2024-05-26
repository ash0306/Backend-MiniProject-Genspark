using CoffeeStoreApplication.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreApplication.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductCategory Category { get; set; }
        public ProductStatus Status { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}

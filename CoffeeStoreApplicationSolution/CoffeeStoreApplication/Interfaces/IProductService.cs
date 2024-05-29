using CoffeeStoreApplication.Models.DTOs.Product;

namespace CoffeeStoreApplication.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAllProducts();
        public Task<IEnumerable<ProductDTO>> GetAllAvailableProducts();
        public Task<ProductDTO> GetById(int id);
        public Task<ProductDTO> GetByName(string name);
        public Task<IEnumerable<ProductDTO>> GetProductsByCategory(string category);
        public Task<ProductDTO> AddProduct(ProductDTO productDTO);
        public Task<ProductPriceDTO> UpdatePrice(ProductPriceDTO productPriceDTO);
        public Task<ProductStatusDTO> UpdateProductStatus(ProductStatusDTO productStatusDTO);
        public Task<ProductStockDTO> UpdateProductStock(ProductStockDTO productStockDTO);
        public Task<IEnumerable<IGrouping<string, CustomerProductDTO>>> GetProductMenu();
    }
}

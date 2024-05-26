using AutoMapper;
using CoffeeStoreApplication.Exceptions.ProductExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Product;
using CoffeeStoreApplication.Models.Enum;

namespace CoffeeStoreApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<int, Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<int, Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> AddProduct(ProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            product.Category = (ProductCategory)Enum.Parse(typeof(ProductCategory), productDTO.Category);
            product.Status = (ProductStatus)Enum.Parse(typeof(ProductStatus), productDTO.Status);

            var newProduct = await _repository.Add(product);
            if(newProduct == null)
            {
                throw new UnableToAddProductException("Unable to add product at the moment");
            }

            productDTO = _mapper.Map<ProductDTO>(newProduct);
            return productDTO;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _repository.GetAll();
            if(products == null)
            {
                throw new NoProductsFoundException($"No products found!!");
            }
            IList<ProductDTO> result = new List<ProductDTO>();
            foreach (var item in products)
            {
                result.Add(_mapper.Map<ProductDTO>(item));
            }
            return result;
        }
        
        public async Task<IEnumerable<ProductDTO>> GetAllAvailableProducts()
        {
            var products = (await _repository.GetAll()).Where(p => p.Status == ProductStatus.Available);
            if(products == null)
            {
                throw new NoProductsFoundException($"No products found!!");
            }
            IList<ProductDTO> result = new List<ProductDTO>();
            foreach (var item in products)
            {
                result.Add(_mapper.Map<ProductDTO>(item));
            }
            return result;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = await _repository.GetById(id);
            if(product == null)
            {
                throw new NoSuchProductException($"No product with ID {id} exists");
            }
            ProductDTO productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(string category)
        {
            IList<Product> products = (await _repository.GetAll()).Where(p => p.Category.ToString() == category).ToList();

            if (products.Count == 0)
                throw new NoProductsFoundException($"No products found");

            IList<ProductDTO> result = new List<ProductDTO>();
            foreach (var item in products)
            {
                result.Add(_mapper.Map<ProductDTO>(item));
            }

            return result;
        }

        public async Task<ProductPriceDTO> UpdatePrice(ProductPriceDTO productPriceDTO)
        {
            var product = await _repository.GetById(productPriceDTO.Id);
            if (product == null)
                throw new NoSuchProductException($"No product with ID {productPriceDTO.Id} exists");
            
            product.Price = productPriceDTO.Price;
            var updatedProduct = await _repository.Update(product);

            productPriceDTO = _mapper.Map<ProductPriceDTO>(updatedProduct);
            return productPriceDTO;
        }

        public async Task<ProductStatusDTO> UpdateProductStatus(ProductStatusDTO productStatusDTO)
        {
            var product = await _repository.GetById(productStatusDTO.Id);
            if (product == null)
                throw new NoSuchProductException($"No product with ID {productStatusDTO.Id} exists");

            product.Status = (ProductStatus)Enum.Parse(typeof(ProductStatus), productStatusDTO.Status);
            var updatedProduct = await _repository.Update(product);

            productStatusDTO = _mapper.Map<ProductStatusDTO>(updatedProduct);
            return productStatusDTO;
        }

        public async Task<ProductStockDTO> UpdateProductStock(ProductStockDTO productStockDTO)
        {
            var product = await _repository.GetById(productStockDTO.Id);
            if (product == null)
                throw new NoSuchProductException($"No product with ID {productStockDTO.Id} exists");

            product.Stock = productStockDTO.Stock;
            var updatedProduct = await _repository.Update(product);

            productStockDTO = _mapper.Map<ProductStockDTO>(updatedProduct);
            return productStockDTO;
        }
    }
}

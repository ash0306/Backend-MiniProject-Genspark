﻿using AutoMapper;
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
        private readonly ILogger<ProductService> _logger;

        public ProductService(IRepository<int, Product> repository, IMapper mapper, ILogger<ProductService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="productDTO">ProductDTO object containing product details</param>
        /// <returns>ProductDTO object containing added product details</returns>
        /// <exception cref="UnableToAddProductException">If unable to add the product</exception>
        public async Task<ProductDTO> AddProduct(ProductDTO productDTO)
        {
            var prod = (await _repository.GetAll()).FirstOrDefault(p => p.Name == productDTO.Name);
            if(prod != null)
            {
                throw new UnableToAddProductException("Product with that name already exists");
            }
            Product product = _mapper.Map<Product>(productDTO);
            product.Category = (ProductCategory)Enum.Parse(typeof(ProductCategory), productDTO.Category);
            product.Status = (ProductStatus)Enum.Parse(typeof(ProductStatus), productDTO.Status);

            var newProduct = await _repository.Add(product);
            if(newProduct == null)
            {
                _logger.LogError("Unable to add product");
                throw new UnableToAddProductException("Unable to add product at the moment");
            }

            productDTO = _mapper.Map<ProductDTO>(newProduct);
            return productDTO;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>List of ProductDTO objects containing all product details</returns>
        /// <exception cref="NoProductsFoundException">If no products are found</exception>
        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _repository.GetAll();
            if(products == null)
            {
                _logger.LogError("No products found");
                throw new NoProductsFoundException($"No products found!!");
            }
            IList<ProductDTO> result = new List<ProductDTO>();
            foreach (var item in products)
            {
                result.Add(_mapper.Map<ProductDTO>(item));
            }
            return result;
        }

        /// <summary>
        /// Gets all available products.
        /// </summary>
        /// <returns>List of ProductDTO objects containing available product details</returns>
        /// <exception cref="NoProductsFoundException">If no products are found</exception>
        public async Task<IEnumerable<ProductDTO>> GetAllAvailableProducts()
        {
            var products = (await _repository.GetAll()).Where(p => p.Status == ProductStatus.Available);
            if(products == null)
            {
                _logger.LogError("No products found");
                throw new NoProductsFoundException($"No products found!!");
            }
            IList<ProductDTO> result = new List<ProductDTO>();
            foreach (var item in products)
            {
                result.Add(_mapper.Map<ProductDTO>(item));
            }
            return result;
        }

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="id">ID of the product</param>
        /// <returns>ProductDTO object containing product details</returns>
        /// <exception cref="NoSuchProductException">If the product is not found</exception>
        public async Task<ProductDTO> GetById(int id)
        {
            var product = await _repository.GetById(id);
            if(product == null)
            {
                _logger.LogError("No product found");
                throw new NoSuchProductException($"No product with ID {id} exists");
            }
            ProductDTO productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        /// <summary>
        /// Gets products by their category.
        /// </summary>
        /// <param name="category">Category of the products</param>
        /// <returns>List of ProductDTO objects containing product details</returns>
        /// <exception cref="NoProductsFoundException">If no products are found</exception>
        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(string category)
        {
            IList<Product> products = (await _repository.GetAll()).Where(p => p.Category.ToString() == category).ToList();

            if (products.Count == 0){
                _logger.LogError("No products found");
                throw new NoProductsFoundException($"No products found");
            }
            IList<ProductDTO> result = new List<ProductDTO>();
            foreach (var item in products)
            {
                result.Add(_mapper.Map<ProductDTO>(item));
            }

            return result;
        }

        /// <summary>
        /// Updates the price of a product.
        /// </summary>
        /// <param name="productPriceDTO">ProductPriceDTO object containing product ID and new price</param>
        /// <returns>ProductPriceDTO object containing updated product price</returns>
        /// <exception cref="NoSuchProductException">If the product is not found</exception>
        public async Task<ProductPriceDTO> UpdatePrice(ProductPriceDTO productPriceDTO)
        {
            var product = (await _repository.GetAll()).FirstOrDefault(p => p.Name == productPriceDTO.Name);
            if (product == null)
            {
                _logger.LogError("No product found");
                throw new NoSuchProductException($"No product with Name {productPriceDTO.Name} exists");
            }
            
            product.Price = productPriceDTO.Price;
            var updatedProduct = await _repository.Update(product);

            productPriceDTO = _mapper.Map<ProductPriceDTO>(updatedProduct);
            return productPriceDTO;
        }

        /// <summary>
        /// Updates the status of a product.
        /// </summary>
        /// <param name="productStatusDTO">ProductStatusDTO object containing product ID and new status</param>
        /// <returns>ProductStatusDTO object containing updated product status</returns>
        /// <exception cref="NoSuchProductException">If the product is not found</exception>
        public async Task<ProductStatusDTO> UpdateProductStatus(ProductStatusDTO productStatusDTO)
        {
            var product = (await _repository.GetAll()).FirstOrDefault(p=>p.Name == productStatusDTO.Name);
            if (product == null)
            {
                _logger.LogError("No product found");
                throw new NoSuchProductException($"No product with Name {productStatusDTO.Name} exists"); 
            }

            product.Status = (ProductStatus)Enum.Parse(typeof(ProductStatus), productStatusDTO.Status);
            if(product.Status == ProductStatus.Unavailable || product.Status == ProductStatus.Discontinued)
            {
                product.Stock = 0;
            }
            else
            {
                product.Stock = (int)productStatusDTO.Stock;
            }
            var updatedProduct = await _repository.Update(product);

            productStatusDTO = _mapper.Map<ProductStatusDTO>(updatedProduct);
            return productStatusDTO;
        }

        /// <summary>
        /// Updates the stock of a product.
        /// </summary>
        /// <param name="productStockDTO">ProductStockDTO object containing product ID and new stock quantity</param>
        /// <returns>ProductStockDTO object containing updated product stock</returns>
        /// <exception cref="NoSuchProductException">If the product is not found</exception>
        public async Task<ProductStockDTO> UpdateProductStock(ProductStockDTO productStockDTO)
        {
            var product = (await _repository.GetAll()).FirstOrDefault(p=> p.Name == productStockDTO.Name);
            if (product == null)
            {
                _logger.LogError("No product found");
                throw new NoSuchProductException($"No product with Name {productStockDTO.Name} exists");
            }

            product.Stock = productStockDTO.Stock;
            if(product.Stock <= 0)
            {
                product.Status = ProductStatus.Unavailable;
            }
            else if(product.Stock >= 1)
            {
                product.Status = ProductStatus.Available;
            }

            var updatedProduct = await _repository.Update(product);

            productStockDTO = _mapper.Map<ProductStockDTO>(updatedProduct);
            return productStockDTO;
        }

        /// <summary>
        /// Gets Product details using name
        /// </summary>
        /// <param name="name">Product name</param>
        /// <returns>ProductDTO containing the product with the given name</returns>
        /// <exception cref="NoSuchProductException">If the product is not found</exception>
        public async Task<ProductDTO> GetByName(string name)
        {
            var product = (await _repository.GetAll()).FirstOrDefault(p=>p.Name ==  name);
            if (product == null)
            {
                _logger.LogError("No product found");
                throw new NoSuchProductException($"No product with name {name} exists");
            }
            ProductDTO productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        public async Task<IEnumerable<IGrouping<string, CustomerProductDTO>>> GetProductMenu()
        {
            var products = (await _repository.GetAll())
                .Where(p => p.Status == ProductStatus.Available);

            IList<CustomerProductDTO> productDTOs = new List<CustomerProductDTO>();
            foreach (var item in products)
            {
                productDTOs.Add(_mapper.Map<CustomerProductDTO>(item));
            }

            var result = productDTOs.GroupBy(p => p.Category);

            return result;
        }
    }
}

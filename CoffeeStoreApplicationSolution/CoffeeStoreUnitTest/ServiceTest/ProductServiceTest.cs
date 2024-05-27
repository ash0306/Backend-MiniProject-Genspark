using AutoMapper;
using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.ProductExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Product;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Repositories;
using CoffeeStoreApplication.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.ServiceTest
{
    public class ProductServiceTest
    {
        private CoffeeStoreContext _context;
        private IRepository<int, Product> _repository;
        private IProductService _productService;
        private IMapper _mapper;
        private MapperConfiguration _mapperConfig;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<CoffeeStoreContext>().UseInMemoryDatabase("dummyProductDB").Options;
            _context = new CoffeeStoreContext(options);

            _mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "CoffeeStoreApplication" }));
            _mapper = _mapperConfig.CreateMapper();

            _repository = new ProductRepository(_context);
            _productService = new ProductService(_repository, _mapper);

            var products = new List<Product>
            {
                new Product { Name = "Espresso", Category = ProductCategory.HotDrinks, Status = ProductStatus.Available, Price = 250, Stock = 10, Description = "" },
                new Product { Name = "Latte", Category = ProductCategory.HotDrinks, Status = ProductStatus.Available, Price = 130, Stock = 5, Description = "" }
            };

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();
        }

        [Test, Order(1)]
        public async Task GetAllProducts_Success()
        {
            var result = await _productService.GetAllProducts();

            Assert.AreEqual(2, result.Count());
        }

        [Test, Order(2)]
        public async Task UpdateProductStock_Success()
        {
            var productStockDTO = new ProductStockDTO { Id = 1, Stock = 15 };
            var result = await _productService.UpdateProductStock(productStockDTO);

            Assert.IsNotNull(result);
            Assert.AreEqual(15, result.Stock);
        }

        [Test, Order(3)]
        public async Task GetProductById_Success()
        {
            var result = await _productService.GetById(2);

            Assert.AreEqual("Latte", result.Name);
        }

        [Test, Order(4)]
        public void GetProductById_Failure()
        {
            Assert.ThrowsAsync<NoSuchProductException>(async () => await _productService.GetById(99));
        }

        [Test, Order(5)]
        public async Task GetProductsByCategory_Success()
        {
            var result = await _productService.GetProductsByCategory("HotDrinks");

            Assert.AreNotEqual(0, result.Count());
        }

        [Test, Order(6)]
        public void GetProductsByCategory_Failure()
        {
            Assert.ThrowsAsync<NoProductsFoundException>(async () => await _productService.GetProductsByCategory("NonExistingCategory"));
        }

        [Test, Order(7)]
        public async Task UpdatePrice_Success()
        {
            var productPriceDTO = new ProductPriceDTO { Id = 1, Price = 3.0f };
            var result = await _productService.UpdatePrice(productPriceDTO);

            Assert.IsNotNull(result);
            Assert.AreEqual(3.0f, result.Price);
        }

        [Test, Order(8)]
        public void UpdatePrice_Failure()
        {
            var productPriceDTO = new ProductPriceDTO { Id = 99, Price = 3.0f };
            Assert.ThrowsAsync<NoSuchProductException>(async () => await _productService.UpdatePrice(productPriceDTO));
        }

        [Test, Order(9)]
        public async Task UpdateProductStatus_Success()
        {
            var productStatusDTO = new ProductStatusDTO { Id = 1, Status = "Unavailable" };
            var result = await _productService.UpdateProductStatus(productStatusDTO);

            Assert.IsNotNull(result);
            Assert.AreEqual("Unavailable", result.Status);
        }

        [Test, Order(10)]
        public void UpdateProductStatus_Failure()
        {
            var productStatusDTO = new ProductStatusDTO { Id = 99, Status = "Unavailable" };
            Assert.ThrowsAsync<NoSuchProductException>(async () => await _productService.UpdateProductStatus(productStatusDTO));
        }

        [Test, Order(11)]
        public void UpdateProductStock_Failure()
        {
            var productStockDTO = new ProductStockDTO { Id = 99, Stock = 15 };
            Assert.ThrowsAsync<NoSuchProductException>(async () => await _productService.UpdateProductStock(productStockDTO));
        }

        [Test, Order(12)]
        public void GetAllProducts_Failure()
        {
            // Clear the database
            foreach (var product in _context.Products)
            {
                _context.Products.Remove(product);
            }
            _context.SaveChanges();

            Assert.ThrowsAsync<NoProductsFoundException>(async () => await _productService.GetAllProducts());
        }
    }
}

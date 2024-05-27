using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.ProductExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.RespositoryTest
{
    public class ProductRepositoryTest
    {
        CoffeeStoreContext context;
        IRepository<int, Product> repository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);
            repository = new ProductRepository(context);
        }

        [Test, Order(1)]
        public async Task AddSuccessTest()
        {
            Product product = new Product()
            {
                Name = "Coffee",
                Description = "Delicious coffee",
                Category = ProductCategory.HotDrinks,
                Status = ProductStatus.Available,
                Price = 99,
                Stock = 100
            };

            var newProduct = await repository.Add(product);
            Assert.AreEqual(1, newProduct.Id);
        }

        [Test, Order(2)]
        public async Task AddFailureTest()
        {
            Product product = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Add(product));
        }

        [Test, Order(3)]
        public async Task DeleteSuccessTest()
        {
            Product product = new Product()
            {
                Name = "Coffee",
                Description = "Delicious coffee",
                Category = ProductCategory.HotDrinks,
                Status = ProductStatus.Available,
                Price = 99,
                Stock = 100
            };

            var newProduct = await repository.Add(product);
            var deletedProduct = await repository.Delete(newProduct.Id);
            Assert.AreEqual(newProduct.Id, deletedProduct.Id);
        }

        [Test, Order(4)]
        public async Task DeleteFailureTest()
        {
            Assert.ThrowsAsync<NoSuchProductException>(async () => await repository.Delete(100));
        }

        [Test, Order(5)]
        public async Task GetAllSuccessTest()
        {
            Product product1 = new Product()
            {
                Name = "Coffee",
                Description = "Delicious coffee",
                Category = ProductCategory.HotDrinks,
                Status = ProductStatus.Available,
                Price = 99,
                Stock = 100
            };

            Product product2 = new Product()
            {
                Name = "Tea",
                Description = "Refreshing tea",
                Category = ProductCategory.HotDrinks,
                Status = ProductStatus.Available,
                Price = 99,
                Stock = 50
            };

            await repository.Add(product1);
            await repository.Add(product2);

            var products = await repository.GetAll();
            Assert.AreEqual(3, products.Count());
        }

        [Test, Order(6)]
        public async Task GetAllFailureTest()
        {
            foreach (var product in context.Products)
            {
                context.Products.Remove(product);
            }
            await context.SaveChangesAsync();
            Assert.ThrowsAsync<NoProductsFoundException>(async () => await repository.GetAll());
        }

        [Test, Order(7)]
        public async Task GetByIdSuccessTest()
        {
            Product product = new Product()
            {
                Name = "Coffee",
                Description = "Delicious coffee",
                Category = ProductCategory.HotDrinks,
                Status = ProductStatus.Available,
                Price = 99,
                Stock = 100
            };

            var newProduct = await repository.Add(product);

            var retrievedProduct = await repository.GetById(newProduct.Id);
            Assert.AreEqual(newProduct.Id, retrievedProduct.Id);
        }

        [Test, Order(8)]
        public async Task GetByIdFailureTest()
        {
            Assert.ThrowsAsync<NoSuchProductException>(async () => await repository.GetById(100));
        }

        [Test, Order(9)]
        public async Task UpdateSuccessTest()
        {
            Product product = new Product()
            {
                Name = "Coffee",
                Description = "Delicious coffee",
                Category = ProductCategory.HotDrinks,
                Status = ProductStatus.Available,
                Price = 99,
                Stock = 100
            };

            var newProduct = await repository.Add(product);
            newProduct.Price = 6.99f;

            var updatedProduct = await repository.Update(newProduct);
            Assert.AreEqual(6.99f, updatedProduct.Price);
        }

        [Test, Order(10)]
        public async Task UpdateFailureTest()
        {
            Product product = new Product()
            {
                Name = "Coffee",
                Description = "Delicious coffee",
                Category = ProductCategory.HotDrinks,
                Status = ProductStatus.Available,
                Price = 99,
                Stock = 100
            };

            var newProduct = await repository.Add(product);
            newProduct.Id = 100;

            Assert.ThrowsAsync<NoSuchProductException>(async () => await repository.Update(newProduct));
        }
    }
}

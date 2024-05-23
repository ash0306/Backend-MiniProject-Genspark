using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.ProductExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreApplication.Repositories
{
    public class ProductRepository : IRepository<int, Product>
    {
        private readonly CoffeeStoreContext _context;

        /// <summary>
        /// Parameterised Constructor to initialize the repository with the CoffeeStore database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductRepository(CoffeeStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds product
        /// </summary>
        /// <param name="item">Product object</param>
        /// <returns>Product object</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input is null</exception>
        /// <exception cref="UnableToAddProductException">Thrown if product cannot be added</exception>
        public async Task<Product> Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Add(item);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
            {
                throw new UnableToAddProductException($"Could not add product with the ID: {item.Id}");
            }

            return item;
        }

        /// <summary>
        /// Deletes the product with the given ID
        /// </summary>
        /// <param name="key">ID of the product to be deleted</param>
        /// <returns>Product object</returns>
        /// <exception cref="NoSuchProductException">Thrown if product with the given ID doesn't exist</exception>
        /// <exception cref="UnableToRemoveProductException">Thrown if product cannot be deleted</exception>
        public async Task<Product> Delete(int key)
        {
            var product = await GetById(key);
            if (product == null)
            {
                throw new NoSuchProductException($"No product with ID {key} exists");
            }
            _context.Remove(product);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToRemoveProductException($"Could not remove product with the ID: {key}");

            return product;
        }

        /// <summary>
        /// Gets a list of all the products
        /// </summary>
        /// <returns>List of all the products</returns>
        /// <exception cref="NoProductsFoundException">Thrown if product doesn't exist</exception>
        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();

            if (products.Count == 0)
                throw new NoProductsFoundException($"No products found!!");

            return products;
        }

        /// <summary>
        /// Gets the details of the product with the given ID
        /// </summary>
        /// <param name="key">ID of the product to be fetched</param>
        /// <returns>Product object</returns>
        /// <exception cref="NoSuchProductException">Thrown if product with the given ID doesn't exist</exception>
        public async Task<Product> GetById(int key)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == key);

            if (product == null)
            {
                throw new NoSuchProductException($"No product with ID {key} exists");
            }
            return product;
        }

        /// <summary>
        /// Updates the details of a given product
        /// </summary>
        /// <param name="item">Product object</param>
        /// <returns>Product object</returns>
        /// <exception cref="NoSuchProductException">Thrown if product with the given ID doesn't exist</exception>
        /// <exception cref="UnableToUpdateProductException">Thrown if product cannot be updated</exception>
        public async Task<Product> Update(Product item)
        {
            var product = await GetById(item.Id);

            if (product == null)
            {
                throw new NoSuchProductException($"No product with ID {item.Id} exists");
            }
            _context.Update(item);

            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToUpdateProductException($"Could not update product with ID : {item.Id}");

            return item;
        }
    }
}
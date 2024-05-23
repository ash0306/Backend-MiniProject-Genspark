using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.OrderExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreApplication.Repositories
{
    public class OrderRepository : IRepository<int, Order>
    {
        private readonly CoffeeStoreContext _context;

        /// <summary>
        /// Parameterised Constructor to initialize the repository with the CoffeeStore database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public OrderRepository(CoffeeStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a order 
        /// </summary>
        /// <param name="item">Order Object</param>
        /// <returns>Order Object</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input is null</exception>
        /// <exception cref="UnableToAddOrderException">Thrown if order cannot be </exception>
        public async Task<Order> Add(Order item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Add(item);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
            {
                throw new UnableToAddOrderException($"Could not add order with the ID: {item.Id}");
            }

            return item;
        }

        /// <summary>
        /// Deletes an order with the given ID
        /// </summary>
        /// <param name="key">ID of te order to be deleted</param>
        /// <returns>Order Object</returns>
        /// <exception cref="NoSuchOrderException">Thrown if order with the given ID doesn't exist</exception>
        /// <exception cref="UnableToRemoveOrderException">Thrown if order cannot be deleted</exception>
        public async Task<Order> Delete(int key)
        {
            var order = await GetById(key);
            if (order == null)
            {
                throw new NoSuchOrderException($"No order with ID {key} exists");
            }
            _context.Remove(order);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToRemoveOrderException($"Could not remove order with the ID: {key}");

            return order;
        }

        /// <summary>
        /// Gets a list of all orders
        /// </summary>
        /// <returns>List of all orders</returns>
        /// <exception cref="NoOrdersFoundException">Thrown if order doesnt exist</exception>
        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _context.Orders.ToListAsync();

            if (orders.Count == 0)
                throw new NoOrdersFoundException($"No orders found!!");

            return orders;
        }

        /// <summary>
        /// Gets the order details for the given ID
        /// </summary>
        /// <param name="key">Id of the order to be fetched</param>
        /// <returns>Order Object</returns>
        /// <exception cref="NoSuchOrderException">Thrown if order with the given ID doesn't exist</exception>
        public async Task<Order> GetById(int key)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == key);

            if (order == null)
            {
                throw new NoSuchOrderException($"No order with ID {key} exists");
            }
            return order;
        }

        /// <summary>
        /// Updates the details of the given order
        /// </summary>
        /// <param name="item">Order Object</param>
        /// <returns>Order Object</returns>
        /// <exception cref="NoSuchOrderException">Thrown if order with the given ID doesn't exist</exception>
        /// <exception cref="UnableToUpdateOrderException">Thrown if order cannot be updated</exception>
        public async Task<Order> Update(Order item)
        {
            var order = await GetById(item.Id);

            if (order == null)
            {
                throw new NoSuchOrderException($"No order with ID {item.Id} exists");
            }
            _context.Update(item);

            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToUpdateOrderException($"Could not update order with ID : {item.Id}");

            return item;
        }
    }
}

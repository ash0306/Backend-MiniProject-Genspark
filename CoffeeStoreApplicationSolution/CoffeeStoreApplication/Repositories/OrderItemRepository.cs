using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Exceptions.OrderItemExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreApplication.Repositories
{
    public class OrderItemRepository : IRepository<int, OrderItem>
    {
        private readonly CoffeeStoreContext _context;

        /// <summary>
        /// Parameterised Constructor to initialize the repository with the CoffeeStore database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public OrderItemRepository(CoffeeStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a Orderitem to the OrderItem table
        /// </summary>
        /// <param name="item">OrderItem object</param>
        /// <returns>OrderItem object</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input is null</exception>
        /// <exception cref="UnableToAddOrderItemException">Thrown if order item cannot be added.</exception>
        public async Task<OrderItem> Add(OrderItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Add(item);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
            {
                throw new UnableToAddOrderItemException($"Could not add order item with the ID: {item.Id}");
            }

            return item;
        }

        /// <summary>
        /// Deletes a OrderItem with the given ID
        /// </summary>
        /// <param name="key">ID of the order item to be deleted</param>
        /// <returns>OrderItem object</returns>
        /// <exception cref="NoSuchOrderItemException">Thrown if order item with the given ID doesn't exist</exception>
        /// <exception cref="UnableToRemoveOrderItemException">Thrown if order item cannot be deleted</exception>
        public async Task<OrderItem> Delete(int key)
        {
            var orderItem = await GetById(key);
            if (orderItem == null)
            {
                throw new NoSuchOrderItemException($"No order item with ID {key} exists");
            }
            _context.Remove(orderItem);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToRemoveOrderItemException($"Could not remove order item with the ID: {key}");

            return orderItem;
        }

        /// <summary>
        /// Gets a list of the order items in the table
        /// </summary>
        /// <returns>List of the order items</returns>
        /// <exception cref="NoOrderItemsFoundException">Thrown if Order Items do not exist</exception>
        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            var orderItems = await _context.OrderItems.ToListAsync();

            if (orderItems.Count == 0)
                throw new NoOrderItemsFoundException($"No order items found!!");

            return orderItems;
        }

        /// <summary>
        /// Gets the order item with given ID
        /// </summary>
        /// <param name="key">ID of the order item to be fetched</param>
        /// <returns>OrderItem object</returns>
        /// <exception cref="NoSuchOrderItemException">Thrown if order item with the given ID doesn't exist</exception>
        public async Task<OrderItem> GetById(int key)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(oi => oi.Id == key);

            if (orderItem == null)
            {
                throw new NoSuchOrderItemException($"No order item with ID {key} exists");
            }
            return orderItem;
        }

        /// <summary>
        /// Updates the order item in OrderItem table
        /// </summary>
        /// <param name="item">OrderItem object</param>
        /// <returns>OrderItem objectOrderItem object</returns>
        /// <exception cref="NoSuchOrderItemException">Thrown if order item with the given ID doesn't exist</exception>
        /// <exception cref="UnableToUpdateOrderItemException">Thrown if order item cannot be updated</exception>
        public async Task<OrderItem> Update(OrderItem item)
        {
            var orderItem = await GetById(item.Id);

            if (orderItem == null)
            {
                throw new NoSuchOrderItemException($"No order item with ID {item.Id} exists");
            }
            _context.Update(item);

            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToUpdateOrderItemException($"Could not update order item with ID : {item.Id}");

            return item;
        }
    }
}

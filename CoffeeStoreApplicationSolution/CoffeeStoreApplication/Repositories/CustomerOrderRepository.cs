using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions;
using CoffeeStoreApplication.Exceptions.CustomerOrderExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreApplication.Repositories
{
    public class CustomerOrderRepository : IRepository<int, CustomerOrder>
    {
        private readonly CoffeeStoreContext _context;

        /// <summary>
        /// Parameterised Constructor to initialize the repository with the CoffeeStore database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CustomerOrderRepository(CoffeeStoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds a customerOrder to the CustomerOrders table
        /// </summary>
        /// <param name="item">CustomerOrder object</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the input is null</exception>
        /// <exception cref="UnableToAddCustomerOrderException">Thrown if customer order cannot be added</exception>
        public async Task<CustomerOrder> Add(CustomerOrder item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Add(item);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
            {
                throw new UnableToAddCustomerOrderException($"Could not add customer order with the ID: {item.Id}");
            }

            return item;
        }

        /// <summary>
        /// Deletes a customerOrder from the CustomerOrders table
        /// </summary>
        /// <param name="key">CustomerOrder to be deleted</param>
        /// <returns></returns>
        /// <exception cref="NoSuchCustomerOrderException">Thrown if customer order with the given ID doesn't exist</exception>
        /// <exception cref="UnableToRemoveCustomerOrderException">Thrown if customer order cannot be deleted</exception>
        public async Task<CustomerOrder> Delete(int key)
        {
            var customerOrder = await GetById(key);
            if (customerOrder == null)
            {
                throw new NoSuchCustomerOrderException($"No customer order with ID {key} exists");
            }
            _context.Remove(customerOrder);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToRemoveCustomerOrderException($"Could not remove customer order with the ID: {key}");

            return customerOrder;
        }

        /// <summary>
        /// Gets a list of all CustomerOrders
        /// </summary>
        /// <returns>List of all CustomerOrders</returns>
        /// <exception cref="NoCustomerOrdersFoundException">Thrown if customer order doesn't exist</exception>
        public async Task<IEnumerable<CustomerOrder>> GetAll()
        {
            var customerOrders = await _context.CustomerOrders.ToListAsync();

            if (customerOrders.Count == 0)
                throw new NoCustomerOrdersFoundException($"No customer orders found!!");

            return customerOrders;
        }

        /// <summary>
        /// Gets the CustomerOrder with the given ID
        /// </summary>
        /// <param name="key">ID of the CustomerOrder to be fetched</param>
        /// <returns>CustomerOrder object</returns>
        /// <exception cref="NoSuchCustomerOrderException">Thrown if customer order with the given ID doesn't exist</exception>
        public async Task<CustomerOrder> GetById(int key)
        {
            var customerOrder = await _context.CustomerOrders.FirstOrDefaultAsync(co => co.Id == key);

            if (customerOrder == null)
            {
                throw new NoSuchCustomerOrderException($"No customer order with ID {key} exists");
            }
            return customerOrder;
        }

        /// <summary>
        /// Updates the data of the CustomerOrder item witht he given ID
        /// </summary>
        /// <param name="item">CustomerOrder object</param>
        /// <returns>CustomerOrder object</returns>
        /// <exception cref="NoSuchCustomerOrderException">Thrown if customer order with the given ID doesn't exist</exception>
        /// <exception cref="UnableToUpdateCustomerOrderException">Thrown if customer order cannot be updated</exception>
        public async Task<CustomerOrder> Update(CustomerOrder item)
        {
            var customerOrder = await GetById(item.Id);

            if (customerOrder == null)
            {
                throw new NoSuchCustomerOrderException($"No customer order with ID {item.Id} exists");
            }
            _context.Update(item);

            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToUpdateCustomerOrderException($"Could not update customer order with ID : {item.Id}");

            return item;
        }
    }
}

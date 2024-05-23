using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.CustomerExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoffeeStoreApplication.Repositories
{
    public class CustomerRepository : IRepository<int, Customer>
    {
        private readonly CoffeeStoreContext _context;

        /// <summary>
        /// Parameterised Constructor to initialize the repository with the CoffeeStore database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CustomerRepository(CoffeeStoreContext context) 
        {
            _context = context;
        }

        /// <summary>
        /// Adds a customer to the Customers table
        /// </summary>
        /// <param name="item">Customer object to be added</param>
        /// <returns>Customer object</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input is null</exception>
        /// <exception cref="UnableToAddCustomerException">Thrown if customer cannot be added</exception>
        public async Task<Customer> Add(Customer item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Add(item);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
            {
                throw new UnableToAddCustomerException($"Could not add customer with the ID: {item.Id}");
            }

            return item;

        }

        /// <summary>
        /// Deletes a customer from the Customers table
        /// </summary>
        /// <param name="key">ID of the customer to be deleted</param>
        /// <returns>Customer object</returns>
        /// <exception cref="NoSuchCustomerException">Thrown if customer with the given ID doesn't exist</exception>
        /// <exception cref="UnableToRemoveCustomerException">Thrown if customer cannot be deleted</exception>
        public async Task<Customer> Delete(int key)
        {
            var customer = await GetById(key);
            if(customer == null)
            {
                throw new NoSuchCustomerException($"No customer with ID {key} exists");
            }
            _context.Remove(customer);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToRemoveCustomerException($"Could not remove customer with the ID: {key}");

            return customer;
        }

        /// <summary>
        /// Gets a list of all the customers in the Customers table
        /// </summary>
        /// <returns>List of customers</returns>
        /// <exception cref="NoCustomersFoundException">Thrown if a customer doesn't exist</exception>
        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();

            if (customers.Count == 0)
                throw new NoCustomersFoundException($"No customers found!!");

            return customers;
        }

        /// <summary>
        /// Gets the customer with the specified ID from the Customers table
        /// </summary>
        /// <param name="key">ID of the customer to be found</param>
        /// <returns>Customer Object</returns>
        /// <exception cref="NoSuchCustomerException">Thrown if customer with the given ID doesn't exist</exception>
        public async Task<Customer> GetById(int key)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == key);

            if(customer == null)
            {
                throw new NoSuchCustomerException($"No customer with ID {key} exists");
            }
            return customer;
        }

        /// <summary>
        /// Updates the customer with the specified ID from the Customers table
        /// </summary>
        /// <param name="item">Customer object to be updated</param>
        /// <returns>Customer Object</returns>
        /// <exception cref="NoSuchCustomerException">Thrown if customer with the given ID doesn't exist</exception>
        /// <exception cref="UnableToUpdateCustomerException">Thrown if customer cannot be updated</exception>
        public async Task<Customer> Update(Customer item)
        {
            var customer = await GetById(item.Id);

            if (customer == null)
            {
                throw new NoSuchCustomerException($"No customer with ID {item.Id} exists");
            }
            _context.Update(item);

            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToUpdateCustomerException($"Could not update customer with ID : {item.Id}");

            return item;
        }
    }
}
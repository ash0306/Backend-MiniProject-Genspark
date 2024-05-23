using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.EmployeeExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreApplication.Repositories
{
    public class EmployeeRepository : IRepository<int, Employee>
    {
        private readonly CoffeeStoreContext _context;

        /// <summary>
        /// Parameterised Constructor to initialize the repository with the CoffeeStore database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public EmployeeRepository(CoffeeStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an Employee to the Employees table
        /// </summary>
        /// <param name="item">Employee object</param>
        /// <returns>Employee object</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input is null</exception>
        /// <exception cref="UnableToAddEmployeeException">Thrown if employee cannot be added</exception>
        public async Task<Employee> Add(Employee item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Add(item);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
            {
                throw new UnableToAddEmployeeException($"Could not add employee with the ID: {item.Id}");
            }

            return item;
        }

        /// <summary>
        /// Deletes the employee with the given ID from the Employees table
        /// </summary>
        /// <param name="key">ID of the employee to be deleted</param>
        /// <returns>Employee object</returns>
        /// <exception cref="NoSuchEmployeeException">Thrown if employee with the given ID doesn't exist</exception>
        /// <exception cref="UnableToRemoveEmployeeException">Thrown if employee cannot be deleted</exception>
        public async Task<Employee> Delete(int key)
        {
            var employee = await GetById(key);
            if (employee == null)
            {
                throw new NoSuchEmployeeException($"No employee with ID {key} exists");
            }
            _context.Remove(employee);
            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToRemoveEmployeeException($"Could not remove employee with the ID: {key}");

            return employee;
        }

        /// <summary>
        /// Gets a list of the employees
        /// </summary>
        /// <returns>List of the employees</returns>
        /// <exception cref="NoEmployeesFoundException">Thrown if Employees do not exist</exception>
        public async Task<IEnumerable<Employee>> GetAll()
        {
            var employees = await _context.Employees.ToListAsync();

            if (employees.Count == 0)
                throw new NoEmployeesFoundException($"No employees found!!");

            return employees;
        }

        /// <summary>
        /// Gets the employee with the given ID
        /// </summary>
        /// <param name="key">ID of the employee to be fetched</param>
        /// <returns>Employee object</returns>
        /// <exception cref="NoSuchEmployeeException">Thrown if employee with the given ID doesn't exist</exception>
        public async Task<Employee> GetById(int key)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == key);

            if (employee == null)
            {
                throw new NoSuchEmployeeException($"No employee with ID {key} exists");
            }
            return employee;
        }

        /// <summary>
        /// Updates an employee with the given details
        /// </summary>
        /// <param name="item">Employee object</param>
        /// <returns>Employee object</returns>
        /// <exception cref="NoSuchEmployeeException">Thrown if employee with the given ID doesn't exist</exception>
        /// <exception cref="UnableToUpdateEmployeeException">Thrown if employee cannot be updated</exception>
        public async Task<Employee> Update(Employee item)
        {
            var employee = await GetById(item.Id);

            if (employee == null)
            {
                throw new NoSuchEmployeeException($"No employee with ID {item.Id} exists");
            }
            _context.Update(item);

            int noOfRowsAffected = await _context.SaveChangesAsync();

            if (noOfRowsAffected <= 0)
                throw new UnableToUpdateEmployeeException($"Could not update employee with ID : {item.Id}");

            return item;
        }
    }
}

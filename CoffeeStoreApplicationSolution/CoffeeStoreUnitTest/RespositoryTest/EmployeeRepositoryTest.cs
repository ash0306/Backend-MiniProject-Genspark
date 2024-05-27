using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.EmployeeExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.RespositoryTest
{
    public class EmployeeRepositoryTest
    {
        CoffeeStoreContext context;
        IRepository<int, Employee> repository;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);
            repository = new EmployeeRepository(context);
        }

        [Test, Order(1)]
        public async Task AddSuccessTest()
        {
            var hmac = new HMACSHA512();
            Employee employee = new Employee()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Status = EmployeeStatus.Active,
                DateOfBirth = DateTime.Now,
                Role = RoleType.Admin,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("johndoe123"))
            };

            var newEmployee = await repository.Add(employee);
            Assert.AreEqual(1, newEmployee.Id);
        }

        [Test, Order(2)]
        public async Task AddFailureTest()
        {
            Employee employee = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Add(employee));
        }

        [Test, Order(3)]
        public async Task DeleteSuccessTest()
        {
            var hmac = new HMACSHA512();
            Employee employee = new Employee()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Status = EmployeeStatus.Active,
                DateOfBirth = DateTime.Now,
                Role = RoleType.Manager,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("johndoe123"))
            };

            var newEmployee = await repository.Add(employee);
            var deletedEmployee = await repository.Delete(newEmployee.Id);
            Assert.AreEqual(newEmployee.Id, deletedEmployee.Id);
        }

        [Test, Order(4)]
        public async Task DeleteFailureTest()
        {
            Assert.ThrowsAsync<NoSuchEmployeeException>(async () => await repository.Delete(100));
        }

        [Test, Order(5)]
        public async Task GetAllSuccessTest()
        {
            var hmac = new HMACSHA512();
            Employee employee1 = new Employee()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Status = EmployeeStatus.Active,
                DateOfBirth = DateTime.Now,
                Role = RoleType.Manager,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("johndoe123"))
            };

            Employee employee2 = new Employee()
            {
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Phone = "0987654321",
                Status = EmployeeStatus.Active,
                DateOfBirth = DateTime.Now,
                Role = RoleType.Admin,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("janesmith123"))
            };

            await repository.Add(employee1);
            await repository.Add(employee2);

            var employees = await repository.GetAll();
            Assert.AreEqual(3, employees.Count());
        }

        [Test, Order(6)]
        public async Task GetAllFailureTest()
        {
            foreach (var employee in context.Employees)
            {
                context.Employees.Remove(employee);
            }
            await context.SaveChangesAsync();
            Assert.ThrowsAsync<NoEmployeesFoundException>(async () => await repository.GetAll());
        }

        [Test, Order(7)]
        public async Task GetByIdSuccessTest()
        {
            var hmac = new HMACSHA512();
            Employee employee = new Employee()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Status = EmployeeStatus.Active,
                DateOfBirth = DateTime.Now,
                Role = RoleType.Manager,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("johndoe123"))
            };

            var newEmployee = await repository.Add(employee);

            var retrievedEmployee = await repository.GetById(newEmployee.Id);
            Assert.AreEqual(newEmployee.Id, retrievedEmployee.Id);
        }

        [Test, Order(8)]
        public async Task GetByIdFailureTest()
        {
            Assert.ThrowsAsync<NoSuchEmployeeException>(async () => await repository.GetById(100));
        }

        [Test, Order(9)]
        public async Task UpdateSuccessTest()
        {
            var hmac = new HMACSHA512();
            Employee employee = new Employee()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Status = EmployeeStatus.Active,
                DateOfBirth = DateTime.Now,
                Role = RoleType.Manager,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("johndoe123"))
            };

            var newEmployee = await repository.Add(employee);
            newEmployee.Phone = "9876543210";

            var updatedEmployee = await repository.Update(newEmployee);
            Assert.AreEqual("9876543210", updatedEmployee.Phone);
        }

        [Test, Order(10)]
        public async Task UpdateFailureTest()
        {
            var hmac = new HMACSHA512();
            Employee employee = new Employee()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Status = EmployeeStatus.Active,
                DateOfBirth = DateTime.Now,
                Role = RoleType.Manager,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("johndoe123"))
            };

            var newEmployee = await repository.Add(employee);
            newEmployee.Id = 100;

            Assert.ThrowsAsync<NoSuchEmployeeException>(async () => await repository.Update(newEmployee));
        }
    }
}

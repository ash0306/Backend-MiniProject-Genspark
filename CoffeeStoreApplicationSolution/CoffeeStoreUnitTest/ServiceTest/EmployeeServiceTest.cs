using AutoMapper;
using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Exceptions.EmployeeExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Employee;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Repositories;
using CoffeeStoreApplication.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreUnitTest.ServiceTest
{
    public class EmployeeServiceTest
    {
        CoffeeStoreContext context;
        IRepository<int, Employee> repository;
        IEmployeeService employeeService;
        IMapper mapper;
        MapperConfiguration mapperConfig;

        [SetUp]
        public async Task Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("dummyEmployeeDB");
            context = new CoffeeStoreContext(optionsBuilder.Options);

            mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "CoffeeStoreApplication" }));
            mapper = mapperConfig.CreateMapper();

            repository = new EmployeeRepository(context);
            employeeService = new EmployeeService(repository, mapper);

            #region SeedDatabase
            var hmac = new HMACSHA512();
            Employee employee1 = new Employee()
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Role = RoleType.Manager,
                Salary = 30000,
                Status = EmployeeStatus.Active,
                DateOfBirth = DateTime.Now,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("employee123"))
            };
            Employee employee2 = new Employee()
            {
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Phone = "0987654321",
                Role = RoleType.Barista,
                Salary = 50000,
                Status = EmployeeStatus.Active,
                DateOfBirth = DateTime.Now,
                PasswordHashKey = hmac.Key,
                HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("employee123"))
            };

            await repository.Add(employee1);
            await repository.Add(employee2);

            #endregion
        }

        [Test, Order(1)]
        public async Task GetAllSuccessTest()
        {
            var result = await employeeService.GetAllEmployees();

            Assert.AreEqual(2, result.Count());
        }

        [Test, Order(2)]
        public async Task GetByIdSuccessTest()
        {
            var result = await employeeService.GetEmployeeById(1);
            Assert.AreEqual("John Doe", result.Name);
        }

        [Test, Order(3)]
        public async Task GetByIdFailureTest()
        {
            Assert.ThrowsAsync<NoSuchEmployeeException>(async () => await employeeService.GetEmployeeById(99));
        }

        [Test, Order(4)]
        public async Task UpdateSalarySuccessTest()
        {
            EmployeeSalaryDTO dto = new EmployeeSalaryDTO()
            {
                EmployeeId = 2,
                EmployeeSalary = 55000
            };
            var result = await employeeService.UpdateSalary(dto);
            Assert.AreEqual(result.EmployeeId, dto.EmployeeId);
        }

        [Test, Order(5)]
        public async Task UpdateSalaryFailureTest()
        {
            EmployeeSalaryDTO dto = new EmployeeSalaryDTO()
            {
                EmployeeId = 99,
                EmployeeSalary = 55000
            };
            Assert.ThrowsAsync<NoSuchEmployeeException>(async () => await employeeService.UpdateSalary(dto));
        }

        [Test, Order(6)]
        public async Task ActivateEmployeeSuccessTest()
        {
            var result = await employeeService.ActivateEmployee(2);
            Assert.AreEqual("Active", result.EmployeeStatus);
        }

        [Test, Order(7)]
        public async Task ActivateEmployeeFailureTest()
        {
            Assert.ThrowsAsync<NoSuchEmployeeException>(async () => await employeeService.ActivateEmployee(99));
        }

        [Test, Order(8)]
        public async Task DeactivateEmployeeSuccessTest()
        {
            var result = await employeeService.DeactivateEmployee(1);
            Assert.AreEqual("Inactive", result.EmployeeStatus);
        }

        [Test, Order(9)]
        public async Task DeactivateEmployeeFailureTest()
        {
            Assert.ThrowsAsync<NoSuchEmployeeException>(async () => await employeeService.DeactivateEmployee(99));
        }

        [Test, Order(10)]
        public async Task GetAllFailureTest()
        {
            foreach (var employee in context.Employees)
            {
                context.Employees.Remove(employee);
            }
            await context.SaveChangesAsync();

            Assert.ThrowsAsync<NoEmployeesFoundException>(async () => await employeeService.GetAllEmployees());
        }
    }
}

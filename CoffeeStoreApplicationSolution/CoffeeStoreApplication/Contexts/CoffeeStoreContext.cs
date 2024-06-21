using CoffeeStoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using CoffeeStoreApplication.Models.Enum;
using System.Security.Cryptography;
using System.Text;

namespace CoffeeStoreApplication.Contexts
{
    public class CoffeeStoreContext : DbContext
    {
        public CoffeeStoreContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set;}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var hmac = new HMACSHA512();

            modelBuilder.Entity<Customer>().HasData(
                new Customer() 
                { 
                    Id = 101, 
                    Name = "Andrew", 
                    Email = "andrew@gmail.com", 
                    DateOfBirth = new DateTime(2000, 2, 12), 
                    Phone = "9891278439",
                    LoyaltyPoints = 0,
                    PasswordHashKey = hmac.Key,
                    HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("andrew123")),
                }
                );


            modelBuilder.Entity<Employee>().HasData(
                new Employee() 
                { 
                    Id = 201, 
                    Name = "Abby", 
                    Email = "abby@gmail.com", 
                    DateOfBirth = new DateTime(1997, 12, 12), 
                    Phone = "9876543298", 
                    Role = RoleType.Admin, 
                    Salary = 60000, 
                    Status = EmployeeStatus.Active,
                    PasswordHashKey = hmac.Key,
                    HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("abby123"))
                },
                new Employee()
                {
                    Id = 202,
                    Name = "David",
                    Email = "david@gmail.com",
                    DateOfBirth = new DateTime(1995, 4, 26),
                    Phone = "9988776655",
                    Role = RoleType.Admin,
                    Salary = 80000,
                    Status = EmployeeStatus.Active,
                    PasswordHashKey = hmac.Key,
                    HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes("david123"))
                }
                );


            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 301,
                    Name = "Espresso",
                    Description = "A strong, full-bodied coffee made with finely ground coffee beans and brewed under high pressure. Perfect for a quick energy boost.",
                    Category = ProductCategory.HotDrinks,
                    Price = 100,
                    Stock = 50,
                    Status = ProductStatus.Available,
                    Image = "https://img.freepik.com/free-vector/realistic-cup-black-brewed-coffee-saucer-vector-illustration_1284-66002.jpg?t=st=1718614373~exp=1718617973~hmac=a5c17f4a2e84d0f7b60ef86b88bb2776f8d0aa34a46139ea578010a7d37517a1&w=740"
                },
                new Product()
                {
                    Id = 302,
                    Name = "Cappuccino",
                    Description = "A classic Italian coffee drink made with equal parts espresso, steamed milk, and foamed milk. Smooth and creamy with a rich flavor.",
                    Category = ProductCategory.HotDrinks,
                    Price = 130,
                    Stock = 50,
                    Status = ProductStatus.Available,
                    Image = "https://images.unsplash.com/photo-1602320574582-741740d4fcd7?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product()
                {
                    Id = 303,
                    Name = "Iced Americano",
                    Description = "A refreshing iced coffee made by combining rich espresso with cold water and ice. A perfect drink for coffee lovers to enjoy on a hot day.",
                    Category = ProductCategory.ColdDrinks,
                    Price = 110,
                    Stock = 50,
                    Status = ProductStatus.Available,
                    Image = "https://images.unsplash.com/photo-1581996323441-538096e854b9?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Product()
                {
                    Id = 304,
                    Name = "Veg Sandwich",
                    Description = "A delicious sandwich filled with fresh vegetables, cheese, and a hint of seasoning. Ideal for a quick and healthy snack.",
                    Category = ProductCategory.Snacks,
                    Price = 120,
                    Stock = 50,
                    Status = ProductStatus.Available,
                    Image = "https://plus.unsplash.com/premium_photo-1671559021919-19d9610c8cad?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
                );


            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerOrder>()
                .HasOne(co => co.Customer)
                .WithMany(c => c.CustomerOrders)
                .HasForeignKey(co => co.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerOrder>()
                .HasOne(co => co.Order)
                .WithOne(o => o.CustomerOrder)
                .HasForeignKey<CustomerOrder>(co => co.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
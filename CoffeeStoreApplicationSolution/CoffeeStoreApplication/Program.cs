using CoffeeStoreApplication.Contexts;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.DTOs.Employee;
using CoffeeStoreApplication.Repositories;
using CoffeeStoreApplication.Services;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Contexts
            builder.Services.AddDbContext<CoffeeStoreContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
                );
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, Customer>, CustomerRepository>();
            builder.Services.AddScoped<IRepository<int, CustomerOrder>, CustomerOrderRepository>();
            builder.Services.AddScoped<IRepository<int, Product>, ProductRepository>();
            builder.Services.AddScoped<IRepository<int, Order>, OrderRepository>();
            builder.Services.AddScoped<IRepository<int, OrderItem>, OrderItemRepository>();
            builder.Services.AddScoped<IRepository<int, Employee>, EmployeeRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<ITokenService, TokenService>();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

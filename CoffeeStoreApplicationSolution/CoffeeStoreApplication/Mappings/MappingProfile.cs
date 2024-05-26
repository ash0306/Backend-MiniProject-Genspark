using AutoMapper;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.DTOs.CustomerOrder;
using CoffeeStoreApplication.Models.DTOs.Employee;
using CoffeeStoreApplication.Models.DTOs.Order;
using CoffeeStoreApplication.Models.DTOs.OrderItems;
using CoffeeStoreApplication.Models.DTOs.Product;

namespace CoffeeStoreApplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Customer Mappings
            CreateMap<Customer, CustomerLoginDTO>().ReverseMap();
            CreateMap<CustomerLoginDTO, CustomerLoginReturnDTO>().ReverseMap();
            CreateMap<Customer, CustomerLoginReturnDTO>().ReverseMap();
            CreateMap<Customer, CustomerRegisterDTO>().ReverseMap();
            CreateMap<CustomerRegisterDTO, CustomerRegisterReturnDTO>().ReverseMap();
            CreateMap<Customer, CustomerRegisterReturnDTO>().ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            #endregion

            #region Employee Mappings
            CreateMap<Employee, EmployeeLoginDTO>().ReverseMap();
            CreateMap<EmployeeLoginDTO, EmployeeLoginReturnDTO>().ReverseMap();
            CreateMap<Employee, EmployeeLoginReturnDTO>().ReverseMap();
            CreateMap<Employee, EmployeeRegisterDTO>().ReverseMap();
            CreateMap<EmployeeRegisterDTO, EmployeeRegisterReturnDTO>().ReverseMap();
            CreateMap<Employee, EmployeeRegisterReturnDTO>().ReverseMap();

            CreateMap<Employee, EmployeeSalaryDTO>().ReverseMap();
            CreateMap<Employee, EmployeeStatusDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            #endregion

            #region Product Mappings
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductPriceDTO>().ReverseMap();
            CreateMap<Product, ProductStatusDTO>().ReverseMap();
            CreateMap<Product, ProductStockDTO>().ReverseMap();
            #endregion

            #region Order Mappings
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderDTO, OrderReturnDTO>().ReverseMap();
            CreateMap<Order, OrderReturnDTO>().ReverseMap();
            CreateMap<Order, OrderStatusDTO>().ReverseMap();
            #endregion

            #region CustomerOrder Mappings
            CreateMap<CustomerOrder, CustomerOrderDTO>().ReverseMap();
            CreateMap<CustomerOrder, CustomerOrderReturnDTO>().ReverseMap();
            #endregion

            #region OrderItem Mappings
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            #endregion
        }
    }
}
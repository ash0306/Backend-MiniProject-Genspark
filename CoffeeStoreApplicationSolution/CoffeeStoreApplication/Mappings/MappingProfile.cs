using AutoMapper;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.DTOs.Employee;

namespace CoffeeStoreApplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerLoginDTO>().ReverseMap();
            CreateMap<CustomerLoginDTO, CustomerLoginReturnDTO>().ReverseMap();
            CreateMap<Customer, CustomerLoginReturnDTO>().ReverseMap();
            CreateMap<Customer, CustomerRegisterDTO>().ReverseMap();
            CreateMap<CustomerRegisterDTO, CustomerRegisterReturnDTO>().ReverseMap();
            CreateMap<Customer, CustomerRegisterReturnDTO>().ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            
            CreateMap<Employee, EmployeeLoginDTO>().ReverseMap();
            CreateMap<EmployeeLoginDTO, EmployeeLoginReturnDTO>().ReverseMap();
            CreateMap<Employee, EmployeeLoginReturnDTO>().ReverseMap();
            CreateMap<Employee, EmployeeRegisterDTO>().ReverseMap();
            CreateMap<EmployeeRegisterDTO, EmployeeRegisterReturnDTO>().ReverseMap();
            CreateMap<Employee, EmployeeRegisterReturnDTO>().ReverseMap();

            CreateMap<Employee, EmployeeSalaryDTO>().ReverseMap();
            CreateMap<Employee, EmployeeStatusDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            
        }
    }
}
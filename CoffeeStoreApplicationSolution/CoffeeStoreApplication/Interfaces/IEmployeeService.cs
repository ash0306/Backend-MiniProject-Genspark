using CoffeeStoreApplication.Models.DTOs.Employee;

namespace CoffeeStoreApplication.Interfaces
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
        public Task<EmployeeDTO> GetEmployeeById(int id);
        public Task<EmployeeSalaryDTO> UpdateSalary(EmployeeSalaryDTO employeeSalaryDTO);
        public Task<EmployeeStatusDTO> ActivateEmployee(int id);
        public Task<EmployeeStatusDTO> DeactivateEmployee(int id);
    }
}

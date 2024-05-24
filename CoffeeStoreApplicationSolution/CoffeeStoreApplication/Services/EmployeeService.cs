using AutoMapper;
using CoffeeStoreApplication.Exceptions.EmployeeExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Employee;
using CoffeeStoreApplication.Models.Enum;

namespace CoffeeStoreApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<int, Employee> _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<int, Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            var employees = await _repository.GetAll();

            if(employees.Count() == 0)
            {
                throw new NoEmployeesFoundException("No employees found");
            }

            IList<EmployeeDTO> result = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                result.Add(_mapper.Map<EmployeeDTO>(employee));
            }
            return result;
        }

        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            var employee = await _repository.GetById(id);

            if(employee == null)
            {
                throw new NoSuchEmployeeException($"No employee with ID {id} exists");
            }

            EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            return employeeDTO;
        }

        public async Task<EmployeeSalaryDTO> UpdateSalary(EmployeeSalaryDTO employeeSalaryDTO)
        {
            Employee employee = await _repository.GetById(employeeSalaryDTO.EmployeeId);
            
            if (employee == null)
            {
                throw new NoSuchEmployeeException($"No employee with the given ID exists");
            }
            employee.Salary = employeeSalaryDTO.EmployeeSalary;

            var updatedEmployee = await _repository.Update(employee);
            employeeSalaryDTO = new EmployeeSalaryDTO()
            {
                EmployeeId = updatedEmployee.Id,
                EmployeeSalary = updatedEmployee.Salary
            };

            return employeeSalaryDTO;
        }

        public async Task<EmployeeStatusDTO> ActivateEmployee(int id)
        {
            Employee employee = await _repository.GetById(id);

            if (employee == null)
            {
                throw new NoSuchEmployeeException($"No employee with the given ID exists");
            }
            employee.Status = EmployeeStatus.Active;

            var updatedEmployee = await _repository.Update(employee);
            EmployeeStatusDTO employeeStatusDTO = new EmployeeStatusDTO()
            {
                EmployeeId = updatedEmployee.Id,
                EmployeeStatus = updatedEmployee.Status.ToString(),
            };

            return employeeStatusDTO;
        }

        public async Task<EmployeeStatusDTO> DeactivateEmployee(int id)
        {
            Employee employee = await _repository.GetById(id);

            if (employee == null)
            {
                throw new NoSuchEmployeeException($"No employee with the given ID exists");
            }
            employee.Status = EmployeeStatus.Inactive;

            var updatedEmployee = await _repository.Update(employee);
            EmployeeStatusDTO employeeStatusDTO = new EmployeeStatusDTO()
            {
                EmployeeId = updatedEmployee.Id,
                EmployeeStatus = updatedEmployee.Status.ToString()
            };

            return employeeStatusDTO;
        }
    }
}

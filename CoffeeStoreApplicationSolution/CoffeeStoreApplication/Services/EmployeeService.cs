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
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IRepository<int, Employee> repository, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of all employees.
        /// </summary>
        /// <returns>List of all employees</returns>
        /// <exception cref="NoEmployeesFoundException">If no employees are found</exception>
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            var employees = await _repository.GetAll();

            if(employees.Count() == 0)
            {
                _logger.LogError("No employees found");
                throw new NoEmployeesFoundException("No employees found");
            }

            IList<EmployeeDTO> result = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                result.Add(_mapper.Map<EmployeeDTO>(employee));
            }
            return result;
        }

        /// <summary>
        /// Gets the details of an employee by their ID.
        /// </summary>
        /// <param name="id">ID of the employee to be found</param>
        /// <returns>EmployeeDTO object containing employee details</returns>
        /// <exception cref="NoSuchEmployeeException">If no employee with the specified ID exists</exception>
        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            var employee = await _repository.GetById(id);

            if(employee == null)
            {
                _logger.LogError("No employee found");
                throw new NoSuchEmployeeException($"No employee with ID {id} exists");
            }

            EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            return employeeDTO;
        }

        /// <summary>
        /// Updates the salary of an employee.
        /// </summary>
        /// <param name="employeeSalaryDTO">EmployeeSalaryDTO object containing employee ID and new salary</param>
        /// <returns>Updated EmployeeSalaryDTO object</returns>
        /// <exception cref="NoSuchEmployeeException">If no employee with the specified ID exists</exception>
        public async Task<EmployeeSalaryDTO> UpdateSalary(EmployeeSalaryDTO employeeSalaryDTO)
        {
            Employee employee = await _repository.GetById(employeeSalaryDTO.EmployeeId);
            
            if (employee == null)
            {
                _logger.LogError("No employee found");
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

        /// <summary>
        /// Activates an employee.
        /// </summary>
        /// <param name="id">ID of the employee to be activated</param>
        /// <returns>EmployeeStatusDTO object containing the updated status</returns>
        /// <exception cref="NoSuchEmployeeException">If no employee with the specified ID exists</exception>
        public async Task<EmployeeStatusDTO> ActivateEmployee(int id)
        {
            Employee employee = await _repository.GetById(id);

            if (employee == null)
            {
                _logger.LogError("No employee found");
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

        /// <summary>
        /// Deactivates an employee.
        /// </summary>
        /// <param name="id">ID of the employee to be deactivated</param>
        /// <returns>EmployeeStatusDTO object containing the updated status</returns>
        /// <exception cref="NoSuchEmployeeException">If no employee with the specified ID exists</exception>
        public async Task<EmployeeStatusDTO> DeactivateEmployee(int id)
        {
            Employee employee = await _repository.GetById(id);

            if (employee == null)
            {
                _logger.LogError("No employee found");
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

        public async Task<IEnumerable<EmployeeDTO>> GetAllAdmins()
        {
            var employees = (await _repository.GetAll()).Where(e=>e.Role == RoleType.Admin);

            if (employees.Count() == 0)
            {
                _logger.LogError("No employees found");
                throw new NoEmployeesFoundException("No employees found");
            }

            IList<EmployeeDTO> result = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                result.Add(_mapper.Map<EmployeeDTO>(employee));
            }
            return result;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllManagers()
        {
            var employees = (await _repository.GetAll()).Where(e => e.Role == RoleType.Manager);

            if (employees.Count() == 0)
            {
                _logger.LogError("No employees found");
                throw new NoEmployeesFoundException("No employees found");
            }

            IList<EmployeeDTO> result = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                result.Add(_mapper.Map<EmployeeDTO>(employee));
            }
            return result;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllBaristas()
        {
            var employees = (await _repository.GetAll()).Where(e => e.Role == RoleType.Barista);

            if (employees.Count() == 0)
            {
                _logger.LogError("No employees found");
                throw new NoEmployeesFoundException("No employees found");
            }

            IList<EmployeeDTO> result = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                result.Add(_mapper.Map<EmployeeDTO>(employee));
            }
            return result;
        }
    }
}

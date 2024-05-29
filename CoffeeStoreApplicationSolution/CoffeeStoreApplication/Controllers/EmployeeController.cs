using CoffeeStoreApplication.Exceptions.EmployeeExceptions;
using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Claims;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAll()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees(); 
                return Ok(employees.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getById")]
        [ProducesResponseType(typeof(EmployeeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDTO>> GetById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);
                return Ok(employee);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateSalary")]
        [ProducesResponseType(typeof(EmployeeSalaryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeSalaryDTO>> UpdateSalary(EmployeeSalaryDTO employeeSalary)
        {
            try
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var empId = Convert.ToInt32(claim);
                if (empId == employeeSalary.EmployeeId) {
                    throw new UnableToUpdateEmployeeException("Admin cannot update their own salary");
                }
                var employee = await _employeeService.UpdateSalary(employeeSalary);
                return Ok(employee);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("activateEmployee")]
        [ProducesResponseType(typeof(EmployeeStatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeStatusDTO>> ActivateEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.ActivateEmployee(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("deactivateEmployee")]
        [ProducesResponseType(typeof(EmployeeStatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeStatusDTO>> DeactivateEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.DeactivateEmployee(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getAllAdmins")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllAdmins()
        {
            try
            {
                var employees = await _employeeService.GetAllAdmins();
                return Ok(employees.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Managers")]
        [HttpGet("getAllManagers")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllManagers()
        {
            try
            {
                var employees = await _employeeService.GetAllManagers();
                return Ok(employees.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("getAllBaristas")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllBaristas()
        {
            try
            {
                var employees = await _employeeService.GetAllBaristas();
                return Ok(employees.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }
    }
}

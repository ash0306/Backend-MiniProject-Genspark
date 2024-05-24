using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
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
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getById")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), StatusCodes.Status200OK)]
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
                var employee = await _employeeService.UpdateSalary(employeeSalary);
                return Ok(employee);
            }
            catch(Exception ex)
            {
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
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }
    }
}

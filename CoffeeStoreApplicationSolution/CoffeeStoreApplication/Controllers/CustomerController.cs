using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [Authorize(Roles = "Barista,Manager")]
        [HttpPut("updateLoyaltyPoints")]
        [ProducesResponseType(typeof(LoyaltyPointsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status304NotModified)]
        public async Task<ActionResult<LoyaltyPointsDTO>> UpdateLoyaltyPoints(LoyaltyPointsDTO loyaltyPoints)
        {
            try
            {
                var result = await _customerService.UpdateLoyaltyPoints(loyaltyPoints);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(304, ex.Message));
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("updatePhone")]
        [ProducesResponseType(typeof(UpdatePhoneDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status304NotModified)]
        public async Task<ActionResult<LoyaltyPointsDTO>> UpdatePhone(UpdatePhoneDTO updatePhoneDTO)
        {
            try
            {
                var result = await _customerService.UpdatePhone(updatePhoneDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(304, ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager,Barista")]
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDTO>> GetAll()
        {
            try
            {
                var result = await _customerService.GetAllCustomers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager,Customer")]
        [HttpGet("getById")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDTO>> GetById(int id)
        {
            try
            {
                var result = await _customerService.GetCustomerById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Manager,Barista")]
        [HttpGet("getByEmail")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDTO>> GetByEmail(string email)
        {
            try
            {
                var result = await _customerService.GetCustomerByEmail(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }
    }
}

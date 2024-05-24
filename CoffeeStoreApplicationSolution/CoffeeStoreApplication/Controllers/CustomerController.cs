using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize(Roles = "Barista")]
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
                return NotFound(new ErrorModel(304, ex.Message));
            }
        }
    }
}

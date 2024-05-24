using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerAuthController : ControllerBase
    {
        private readonly IAuthLoginService<CustomerLoginReturnDTO, CustomerLoginDTO> _authLoginService;
        private readonly IAuthRegisterService<CustomerRegisterReturnDTO, CustomerRegisterDTO> _authRegisterService;

        public CustomerAuthController(IAuthLoginService<CustomerLoginReturnDTO, CustomerLoginDTO> authLoginService, IAuthRegisterService<CustomerRegisterReturnDTO, CustomerRegisterDTO> authRegisterService)
        {
            _authLoginService = authLoginService;
            _authRegisterService = authRegisterService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(CustomerRegisterReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerRegisterReturnDTO>> Register(CustomerRegisterDTO registerDTO)
        {
            try
            {
                CustomerRegisterReturnDTO returnDTO = await _authRegisterService.Register(registerDTO, RoleType.Customer);
                return Ok(returnDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(CustomerLoginReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerLoginReturnDTO>> Login(CustomerLoginDTO loginDTO)
        {
            try
            {
                CustomerLoginReturnDTO returnDTO = await _authLoginService.Login(loginDTO);
                return Ok(returnDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
    }
}

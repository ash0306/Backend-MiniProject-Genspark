using CoffeeStoreApplication.Models.DTOs.Employee;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeStoreApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeAuthController : ControllerBase
    {
        private readonly IAuthLoginService<EmployeeLoginReturnDTO, EmployeeLoginDTO> _authLoginService;
        private readonly IAuthRegisterService<EmployeeRegisterReturnDTO, EmployeeRegisterDTO> _authRegisterService;

        public EmployeeAuthController(IAuthLoginService<EmployeeLoginReturnDTO, EmployeeLoginDTO> authLoginService, IAuthRegisterService<EmployeeRegisterReturnDTO, EmployeeRegisterDTO> authRegisterService)
        {
            _authLoginService = authLoginService;
            _authRegisterService = authRegisterService;
        }

        [HttpPost("register/admin")]
        [ProducesResponseType(typeof(EmployeeRegisterReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeRegisterReturnDTO>> RegisterAdmin(EmployeeRegisterDTO registerDTO)
        {
            try
            {
                EmployeeRegisterReturnDTO returnDTO = await _authRegisterService.Register(registerDTO, RoleType.Admin);
                return Ok(returnDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }

        [HttpPost("register/manager")]
        [ProducesResponseType(typeof(EmployeeRegisterReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeRegisterReturnDTO>> RegisterManager(EmployeeRegisterDTO registerDTO)
        {
            try
            {
                EmployeeRegisterReturnDTO returnDTO = await _authRegisterService.Register(registerDTO, RoleType.Manager);
                return Ok(returnDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }

        [HttpPost("register/barista")]
        [ProducesResponseType(typeof(EmployeeRegisterReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeRegisterReturnDTO>> RegisterBarista(EmployeeRegisterDTO registerDTO)
        {
            try
            {
                EmployeeRegisterReturnDTO returnDTO = await _authRegisterService.Register(registerDTO, RoleType.Barista);
                return Ok(returnDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(EmployeeLoginReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeLoginReturnDTO>> Login(EmployeeLoginDTO loginDTO)
        {
            try
            {
                EmployeeLoginReturnDTO returnDTO = await _authLoginService.Login(loginDTO);
                return Ok(returnDTO);

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
    }
}

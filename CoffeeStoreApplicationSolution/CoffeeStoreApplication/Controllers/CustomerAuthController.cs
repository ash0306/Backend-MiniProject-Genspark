﻿using CoffeeStoreApplication.Interfaces;
using CoffeeStoreApplication.Models;
using CoffeeStoreApplication.Models.DTOs.Customer;
using CoffeeStoreApplication.Models.Enum;
using CoffeeStoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreApplication.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class CustomerAuthController : ControllerBase
    {
        private readonly IAuthLoginService<CustomerLoginReturnDTO, CustomerLoginDTO> _authLoginService;
        private readonly IAuthRegisterService<CustomerRegisterReturnDTO, CustomerRegisterDTO> _authRegisterService;
        private readonly ILogger<CustomerAuthController> _logger;

        public CustomerAuthController(IAuthLoginService<CustomerLoginReturnDTO, CustomerLoginDTO> authLoginService, IAuthRegisterService<CustomerRegisterReturnDTO, CustomerRegisterDTO> authRegisterService, ILogger<CustomerAuthController> logger)
        {
            _authLoginService = authLoginService;
            _authRegisterService = authRegisterService;
            _logger = logger;
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
                _logger.LogCritical(ex.Message, ex);
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
                _logger.LogCritical(ex.Message, ex);
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
    }
}

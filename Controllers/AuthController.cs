using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finance_reporter_api.Data;
using finance_reporter_api.Dtos.User;
using finance_reporter_api.Models;
using finance_reporter_api.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace finance_reporter_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<LoadUserDto>>> Register(UserRegisterDto request)
        {
            var response = await _authService.Register(
                new User
                {
                    FirstName = request.Firstname,
                    LastName = request.Lastname,
                    Email = request.Email
                },
                request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<LoadUserDto>>> Login(UserLoginDto request)
        {
            var response = await _authService.Login(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Load current user
        [Authorize]
        [HttpGet("load-user")]
        public async Task<ActionResult<ServiceResponse<LoadUserDto>>> LoadUser()
        {
            var response = await _authService.LoadUser();

            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("user")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteUser()
        {
            var response = await _authService.DeleteUser();

            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }
    }
}
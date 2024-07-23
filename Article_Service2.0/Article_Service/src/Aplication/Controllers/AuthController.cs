using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Dto;
using Domain.Services;
using Domain.Interfaces;
using Article_Service.src.Domain.Interfaces;

namespace Article_Service.src.Aplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (IsValidUser(login.Username, login.Password))
            {
                var token = _jwtService.GenerateToken(login.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private bool IsValidUser(string username, string password)
        {
            // Implement your user validation logic here
            return username == "testuser" && password == "testpassword";
        }
    }
}
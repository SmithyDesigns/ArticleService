using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Dto;
using Domain.Services;
using Domain.Interfaces;
using Article_Service.src.Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (IsValidUser(loginDto.Username, loginDto.Password))
            {
                var token = _jwtService.GenerateToken(loginDto.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private bool IsValidUser(string username, string password)
        {
            //@todo fix this part
            var connectionString = builder.Configuration.GetConnectionString("DbConnection");
            var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer(connectionString)
            .Options;


            using var dbContext = new MyDbContext(options);
            return dbContext.Customers.Any(u => u.Username == username && u.Password == password);
        }
    }
}
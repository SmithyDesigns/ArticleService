using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
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
        private readonly MyDbContext _dbContext;

        public AuthController(IJwtService jwtService, MyDbContext dbContext)
        {
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("Invalid username or password");
            }

            var user = await _dbContext.Customers.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
            
            if (user == null)
            {
                return Unauthorized($"User not found: {loginDto.Username}");
            }

            var isValidPassword = VerifyPassword(loginDto.Password, user.Password);

            if (!isValidPassword)
            {
                return Unauthorized($"Invalid password for user: {loginDto.Username}");
            }

            var token = _jwtService.GenerateToken(loginDto.Username);
            return new JsonResult(new { token, message = "Login successful" });
        }

        private string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            if (string.IsNullOrEmpty(storedHashedPassword))
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHashedPassword);
        }
    }
}

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

            var user = await _dbContext.Customers.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
            
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
            return Ok(new { token, message = "Login successful" });
        }

        private async Task<(bool isValid, string errorMessage)> IsValidUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Username and password are required");
            }

            var user = await _dbContext.Customers.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return (false, $"User not found: {username}");
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return (false, $"User found but password is empty: {username}");
            }

            var isValidPassword = VerifyPassword(password, user.Password);

            return (isValidPassword, isValidPassword ? string.Empty : $"Invalid password for user: {username}");
        }



        private string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty or null", nameof(password));

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            var hashedInputPassword = HashPassword(inputPassword);
            return hashedInputPassword == storedHashedPassword;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpPost("create-customer")]
        public async Task<IActionResult> Create([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null || string.IsNullOrWhiteSpace(customerDto.Username) || string.IsNullOrWhiteSpace(customerDto.Password))
            {
                return BadRequest("Valid username and password are required");
            }

            try
            {
                var customer = await _customerService.Create(customerDto);
                if (customer == null)
                {
                    return StatusCode(500, "Failed to create customer");
                }

                return CreatedAtAction(nameof(Find), new { username = customer.Username }, new { customer.Username, Message = "Customer created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the customer: {ex.Message}");
            }
        }

        [HttpGet("find-customer")]
        public async Task<IActionResult> Find([FromQuery] SearchDto searchDto)
        {
            if (searchDto.Username == null)
            {
                return BadRequest("CustomerDto is required");
            }

            var customer = await _customerService.Find(searchDto);
            return Ok(customer);
        }
    }
}

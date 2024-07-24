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
            _customerService = customerService;
        }

        [HttpPost("create-customer")]
        public async Task<IActionResult> Create([FromBody] CustomerDto customerDto )
        {
            if (customerDto.Password == null || customerDto.Username == null)
            {
                return BadRequest("CustomerDto is required");
            }

            var customer = await _customerService.Create(customerDto);

            var createdcustomerDto = new CustomerDto
            {
                Username = customer.Username,
                Password = customer.Password
            };

            return Ok(createdcustomerDto);
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

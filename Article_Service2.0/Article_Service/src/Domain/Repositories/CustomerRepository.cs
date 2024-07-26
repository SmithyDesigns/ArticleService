using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using Domain.Data;
using Domain.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Article_Service.src.Domain.Entities;
using System.Text;
using System.Security.Cryptography;
using BCrypt.Net;

namespace Domain.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyDbContext _context;

        public CustomerRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Create(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                Username = customerDto.Username,
                Password = HashPassword(customerDto.Password)
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> Find(SearchDto searchDto)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(a => a.Username == searchDto.Username);
            if (customer == null)
            {
                throw new NotFoundException("Customer not found");
            }
            return customer;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}

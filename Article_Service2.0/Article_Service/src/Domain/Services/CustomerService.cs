using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Article_Service.src.Domain.Entities;
using Domain.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Create(CustomerDto customerDto)
        {
            if (string.IsNullOrEmpty(customerDto.Username) || string.IsNullOrEmpty(customerDto.Password))
            {
                throw new ArgumentException("Username and Password are required.", nameof(customerDto));
            }

            try
            {
                return await _customerRepository.Create(customerDto);
            }
            catch (DbUpdateException ex)
            {
                throw new ArgumentException("Failed to create article. Please check the database connection.", nameof(customerDto), ex);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred while creating the article.", nameof(customerDto), ex);
            }
        }

        public async Task<Customer> Find(SearchDto searchDto)
        {
            if (searchDto == null)
            {
                throw new ArgumentNullException(nameof(searchDto), "SearchDto is required");
            }

            try
            {
                return await _customerRepository.Find(searchDto);
            }
            catch (Exception)
            {
                throw new NotFoundException("Customer not found");
            }
        }
    }
}
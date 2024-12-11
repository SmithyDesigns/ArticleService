using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Article_Service.src.Domain.Entities;
using Domain.Dto;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> Create(CustomerDto customerDto);
        Task<Customer> Find(SearchDto searchDto);
    }
}
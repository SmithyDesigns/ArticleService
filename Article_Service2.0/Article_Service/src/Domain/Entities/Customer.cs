using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article_Service.src.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}


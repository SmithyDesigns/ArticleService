using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CustomerDto
    {
        public required string? Username { get; set; }
        public required string? Password { get; set; }
    }
}
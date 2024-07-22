using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CreateDto
    {
        public required string? Title { get; set; }
        public required string? Html { get; set; }
    }
}
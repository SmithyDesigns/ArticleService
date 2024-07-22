using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class FindDto
    {
        private int Id { get; set;}
        public required string? Title { get; set; }
    }
}
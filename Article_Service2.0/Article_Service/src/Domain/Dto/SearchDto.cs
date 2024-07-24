using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class SearchDto
    {
        public required string Username { get; set;}
    }
}
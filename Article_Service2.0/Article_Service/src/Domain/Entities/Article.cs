using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Article
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string? Title { get; set; }
        public required string? Description { get; set; }
    }
}

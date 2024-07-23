using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Article_Service.src.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username);
        bool ValidateToken(string token);
    }
}

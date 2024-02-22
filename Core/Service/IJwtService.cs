using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IJwtService {
        public string? CreateToken(IEnumerable<Claim> claims, DateTime expireDate);
        public IEnumerable<Claim>? ValidateToken(string token); 

    }
}

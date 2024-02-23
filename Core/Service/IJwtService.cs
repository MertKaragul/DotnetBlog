using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IJwtService {
        public string? CreateToken(IEnumerable<Claim> claims, DateTime expireDate);
        public Task<IDictionary<string, object>?> ValidateToken(string token); 

    }
}

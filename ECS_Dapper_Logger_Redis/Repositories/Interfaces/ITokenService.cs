using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string id, string email, string role);
        string GenerateRefreshToken();
    }
}

using MoneyTrack.Core.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Web.Infrastructure.Interfaces
{
    public interface ITokenGenerator
    {
        JwtSecurityToken GenerateToken(UserDto user);
    }
}

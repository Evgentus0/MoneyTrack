using Microsoft.IdentityModel.Tokens;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Web.Infrastructure.Interfaces;
using MoneyTrack.Web.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Web.Infrastructure.Services
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly AppSettings _settings;

        public JwtTokenGenerator(AppSettings settings)
        {
            _settings = settings;
        }

        public JwtSecurityToken GenerateToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Authorization.SecretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            user.Roles.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));

            var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(_settings.Authorization.MinutesToExpiration),
            signingCredentials: credentials
            );

            return token;
        }
    }
}

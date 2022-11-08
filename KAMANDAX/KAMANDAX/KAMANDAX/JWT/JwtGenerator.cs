using KAMANDAX.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KAMANDAX.JWT
{
    public class JwtGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _generator;

        public JwtGenerator(AuthenticationConfiguration configuration, TokenGenerator generator)
        {
            _configuration = configuration;
            _generator = generator;
        }

        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            return _generator.GenerateToken(
                _configuration.Key,
                _configuration.Issuer,
                _configuration.Audience,
                _configuration.ExpireTime,
                claims);
        }
    }
}

using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Utils.Interfaces;

namespace Shared.Utils.Services
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private const int ExpirationMinutes = 30;
        private readonly IConfiguration configurarion;

        public JwtTokenGenerator(IConfiguration configurarion)
        {
            this.configurarion = configurarion;
        }

        public string Generate(string userName, IEnumerable<string> roles)
        {
            var startAt = DateTime.UtcNow;
            var expiration = startAt.AddMinutes(ExpirationMinutes);
            var claims = CreateClaims(userName, roles, startAt, expiration);
            var credentials = CreateSigningCredentials(configurarion["JWT:SecurityKey"]);

            var token = CreateJwtToken(startAt, claims, credentials, expiration);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(
            DateTime notBefore,
            List<Claim> claims,
            SigningCredentials credentials,
            DateTime expiration) =>
            new(
                configurarion["JWT:Issuer"],
                configurarion["JWT:Audience"],
                claims,
                notBefore,
                expires: expiration,
                signingCredentials: credentials
            );

        private static List<Claim> CreateClaims(string userName, IEnumerable<string> roles, DateTime nbf, DateTime iat)
        {

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Nbf, nbf.Ticks.ToString()),
                new(JwtRegisteredClaimNames.Iat, nbf.Ticks.ToString()),
                new(ClaimTypes.Name, userName),
            };
            foreach (var role in roles)
                claims.Add(new(ClaimTypes.Role, role));
            return claims;
        }
        private static SigningCredentials CreateSigningCredentials(string securityKey)
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(securityKey)
                ),
                SecurityAlgorithms.HmacSha256Signature
            );
        }
    }
}

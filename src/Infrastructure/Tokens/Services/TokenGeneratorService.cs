using Application.Tokens.DTOs.Request;
using Application.Tokens.Services.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration configuration;

        public TokenGeneratorService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateJWTToken(GenerateJWTTokenRequest request)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured"));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.User.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, request.User.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (request.Roles != null)
            {
                foreach (var role in request.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(jwtSettings["ExpirationHours"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

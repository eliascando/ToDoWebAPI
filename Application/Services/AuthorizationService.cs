
using Domain.Entities;
using Domain.Interfaces.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Services
{
    public class AuthorizationService
        : IAuthorizationService<Usuario>
    {
        private readonly IConfiguration _configuration;

        public AuthorizationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario user)
        {
            var token = new JwtSecurityTokenHandler();
            string secret = _configuration["JWT:Secret"] ?? throw new System.Exception("No se pudo obtener el secret");
            var key = Encoding.ASCII.GetBytes(secret);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("Id", user.Id.ToString()),
                    new System.Security.Claims.Claim("Nombres", user.Nombres + " "+user.Apellidos),
                    new System.Security.Claims.Claim("Correo", user.Correo)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenGenerated = token.CreateToken(descriptor);
            var tokenString = token.WriteToken(tokenGenerated);
            return tokenString;
        }
    }
}

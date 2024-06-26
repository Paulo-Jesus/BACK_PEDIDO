using EntitiLayer.Models.Entities;
using EntityLayer.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DataLayer.Utilities
{
    public class Utility
    {
        private readonly IConfiguration _configuration;

        public Utility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string encriptarPass(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder builder = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }

        public string generarJWT(Usuario usuario)
        {
            Claim[] userClaims = [
                //new Claim(ClaimTypes.NameIdentifier,usuario.Nombre),
                //new Claim(ClaimTypes.Role,usuario.IdRol.ToString())
                new Claim("Nombre",usuario.Nombre),
                new Claim("Rol",usuario.IdRol.ToString())
            ];

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //SigningCredentials creadentials = new(key, SecurityAlgorithms.HmacSha256Signature); 
            SigningCredentials creadentials = new(key, "HS256");

            JwtSecurityToken jwtConfig = new(
                    claims: userClaims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: creadentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}

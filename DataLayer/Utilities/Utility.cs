using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        public string encriptarContrasena(string texto)
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

        public string generarJWT(string Rol, string Nombre)
        {
            Claim[] userClaims = [
                //new Claim(ClaimTypes.NameIdentifier,usuario.Nombre),
                //new Claim(ClaimTypes.Role,usuario.IdRol.ToString())
                new Claim("Rol",Rol),
                new Claim("Nombre",Nombre)
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

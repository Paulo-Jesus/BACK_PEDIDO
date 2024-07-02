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

        public string EncriptarContrasena(string texto)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(texto));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public string GenerarTexto(int length)
        {
            string ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=[]{}|;:,.<>?";
            Random random = new();
            StringBuilder password = new();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(ValidChars.Length);
                password.Append(ValidChars[index]);
            }

            return password.ToString();
        }

        public string TokenInicioSesion(string Rol, string Nombre)
        {
            Claim[] userClaims = [
                //new Claim(ClaimTypes.NameIdentifier,usuario.Nombre),
                //new Claim(ClaimTypes.Role,usuario.IdRol.ToString())
                new Claim("Rol",Rol),
                new Claim("Nombre",Nombre)
            ];

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            //SigningCredentials creadentials = new(key, SecurityAlgorithms.HmacSha256Signature); 
            SigningCredentials creadentials = new(key, "HS256");

            JwtSecurityToken jwtConfig = new(
                    claims: userClaims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creadentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }

        public string GenerarToken(string texto)
        {
            Claim[] userClaims = [
                //new Claim(ClaimTypes.NameIdentifier,usuario.Nombre),
                //new Claim(ClaimTypes.Role,usuario.IdRol.ToString())
                new Claim("Claim:",texto)
            ];

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            //SigningCredentials creadentials = new(key, SecurityAlgorithms.HmacSha256Signature); 
            SigningCredentials creadentials = new(key, "HS256");

            JwtSecurityToken jwtConfig = new(
                    claims: userClaims,
                    //expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creadentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}

using EntityLayer.Models.DTO;
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

        public string encriptarPass(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }

        //public string generarJWT(UsuarioDTO usuarioDTO)
        //{
        //    var userClaims = new[] 
        //    {
        //        new Claim(ClaimTypes.NameIdentifier,usuarioDTO.Correo)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var creadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        //    var jwtConfig = new JwtSecurityToken(
        //        claims: userClaims,
        //        expires: DateTime.UtcNow.AddDays(1),
        //        signingCredentials: creadentials
        //        );
        //    return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        //}
    }
}

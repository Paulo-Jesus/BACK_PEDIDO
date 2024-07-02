using DataLayer.Repositories.Login;
using DataLayer.Repositories.Seguridad.Usuarios;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.Login
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepository _loginRepository;
        private Response response = new();

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<Response> IniciarSesion(LoginDTO request)
        {
            response = await _loginRepository.IniciarSesion(request);
            return response;
        }


        public bool CompararTokens(string tokenFrontend, string tokenBackend)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("@2024_cl@ve_de@ccesoApl1cac1on@2023_cl@_154920$#@_++&&//_2023$$0o_&%###9000");

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(tokenFrontend, validationParameters, out validatedToken);

                var jwtTokenBackend = tokenHandler.ReadJwtToken(tokenBackend);

                return jwtTokenBackend.RawData.Equals(tokenFrontend);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al comparar tokens: {ex.Message}");
                return false;
            }
        }


    }


}

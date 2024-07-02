using BusinessLayer.Services.Seguridad.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        ILoginService _loginService;

        public TokenController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost("CompararTokens")]
        public bool CompararTokens([FromBody] string tokenFrontend)
        {
            var tokenBackend = Request.Cookies["token"]; // Obtener el token almacenado en las cookies del backend

            var resultado = _loginService.CompararTokens(tokenFrontend, tokenBackend);

            return resultado;
        }




    }
}

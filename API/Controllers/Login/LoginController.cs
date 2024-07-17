using BusinessLayer.Services.Login;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;
using API.Common;

namespace API.Controllers.Login
{
    [Route(APIRoutes.Route)]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly ILoginService _loginService;
        private Response response = new();

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [Route(APIRoutes.IniciarSesionGoogle)]
        [HttpPost]
        public async Task<IActionResult> IniciarSesionGoogle(LoginDTO request)
        {
            response = await _loginService.IniciarSesionGoogle(request);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route(APIRoutes.IniciarSesion)]
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(LoginDTO request)
        {
            response = await _loginService.IniciarSesion(request);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route(APIRoutes.GenerarContrasena)]
        [HttpPost]
        public async Task<IActionResult> GenerarContrasena(string Correo)
        {
            response = await _loginService.GenerarContrasena(Correo);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route(APIRoutes.ComprobarToken)]
        [HttpPost]
        public async Task<IActionResult> ComprobarToekn(string tokenCuerpo)
        {
            response = await _loginService.ComprobarToken(tokenCuerpo);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route(APIRoutes.RestablecerContrasena)]
        [HttpPost]
        public async Task<IActionResult> RestablecerContrasena(string tokenCuerpo, string claveTemporal, string claveNueva)
        {
            response = await _loginService.RestablecerContrasena(tokenCuerpo, claveTemporal, claveNueva);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }
    }
}

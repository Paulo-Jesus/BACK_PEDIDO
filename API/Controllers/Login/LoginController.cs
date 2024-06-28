using BusinessLayer.Services.Login;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly ILoginService _loginService;
        private Response response = new();

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [Route("IniciarSesion")]
        [HttpPost]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO request)
        {
            response = await _loginService.IniciarSesion(request);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route("GenerarContrasena")]
        [HttpPost]
        public async Task<IActionResult> GenerarContrasena([FromBody] string Correo)
        {
            response = await _loginService.GenerarContrasena(Correo);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }
    }
}

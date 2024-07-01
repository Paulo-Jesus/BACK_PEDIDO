using BusinessLayer.Services.Seguridad.Login;
using DataLayer.Repositories.Login;
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

            if (response.Data is LoginResponseData responseData && responseData.Issuccess)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, // Set to true in production for HTTPS
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                Response.Cookies.Append("token", responseData.Token, cookieOptions);
            }

            return Ok(response);

        }
    }
}

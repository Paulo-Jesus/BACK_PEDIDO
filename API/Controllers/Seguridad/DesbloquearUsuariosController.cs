using BusinessLayer.Services.Seguridad.DesbloquearCuenta;
using EntityLayer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Responses;

namespace API.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesbloquearUsuariosController : ControllerBase
    {
        private readonly IUsuarioDcService _usuarioDcService;
        Response response = new();

        public DesbloquearUsuariosController(IUsuarioDcService serviceDC)
        {
            _usuarioDcService = serviceDC;
        }

        [Route(API.Common.APIRoutes.getAPIObtenerUsuariosBloqueados)]
        [HttpGet]
        public async Task<IActionResult> obtenerUsuariosBloqueados()
        {
            response = await _usuarioDcService.obtenerTodosUsuarios();

            if (response.Code == ResponseType.Error) { 
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route(API.Common.APIRoutes.getAPIBuscarUsuarioBloqueado)]
        public async Task<ActionResult> buscarUsuarioBloqueado(string nombreUsuario)
        {
            response = await _usuarioDcService.buscarUsuarioBloqueado(nombreUsuario);

            if (response.Code == ResponseType.Error)
            {
                return BadRequest(response);
            }

            return Ok(new
            {
                response
            });
        }

        [HttpPut]
        [Route(API.Common.APIRoutes.getAPIDesbloquearUsuario)]
        public async Task<ActionResult> DesbloquearUsuario(string correo)
        {
            response = await _usuarioDcService.DesbloquearUsuario(correo);

            if (response.Code == ResponseType.Error)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}

using BusinessLayer.Services.Seguridad.DesbloquearCuenta;
using EntityLayer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Responses;
using API.Common;
namespace API.Controllers.Seguridad
{
    [Route(APIRoutes.Route)]
    [ApiController]
    public class DesbloquearUsuariosController : ControllerBase
    {
        private readonly IUsuarioDcService _usuarioDcService;
        Response response = new();

        public DesbloquearUsuariosController(IUsuarioDcService serviceDC)
        {
            _usuarioDcService = serviceDC;
        }

        [Route(APIRoutes.getAPIObtenerUsuariosBloqueados)]
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
        [Route(APIRoutes.getAPIBuscarUsuarioBloqueado)]
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
        [Route(APIRoutes.getAPIDesbloquearUsuario)]
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

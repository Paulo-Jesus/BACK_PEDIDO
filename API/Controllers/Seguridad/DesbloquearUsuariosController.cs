using BusinessLayer.Services.Seguridad.DesbloquearCuenta;
using EntityLayer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Responses;

namespace API.Controllers.Seguridad
{
    public class DesbloquearUsuariosController : Controller
    {
        Response response = new();
        ResponseType rt = new();
        private readonly UsuarioService _service;

        public DesbloquearUsuariosController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route(API.Common.APIRoutes.getAPIObtenerUsuariosBloqueados)]
        public async Task<ActionResult> obtenerUsuariosBloqueados()
        {
            response = await _service.obtenerTodosUsuarios();

            if (response.Code == ResponseType.Error) { 
                return BadRequest(response);
            }

            return Ok(new
            {
                response
            });
        }

        [HttpPost]
        [Route(API.Common.APIRoutes.getAPIBuscarUsuarioBloqueado)]
        public async Task<ActionResult> buscarUsuarioBloqueado(string nombreUsuario)
        {
            response = await _service.buscarUsuarioBloqueado(nombreUsuario);

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
            response = await _service.DesbloquearUsuario(correo);

            if (response.Code == ResponseType.Error)
            {
                return BadRequest(response);
            }

            return Ok(new
            {
                response
            });
        }

    }
}

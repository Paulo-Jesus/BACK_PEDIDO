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
        public ActionResult<IEnumerable<Response>> obtenerUsuariosBloqueados()
        {
            IEnumerable<UsuarioBlockDTO> data = _service.obtenerTodosUsuarios();

            response.Data = data;
            response.Message = DataLayer.Common.DLMessages.ListadoUsuarios;
            response.Code = ResponseType.Success;

            return Ok(new
            {
                response
            });
        }

        [HttpPost]
        [Route(API.Common.APIRoutes.getAPIBuscarUsuarioBloqueado)]
        public ActionResult<UsuarioBlockDTO> buscarUsuarioBloqueado(string nombreUsuario)
        {
            UsuarioBlockDTO data = _service.buscarUsuarioBloqueado(nombreUsuario);
            return Ok(new
            {
                data
            });
        }

        [HttpPut]
        [Route(API.Common.APIRoutes.getAPIDesbloquearUsuario)]
        public ActionResult<String> DesbloquearUsuario(string nombreUsuario)
        {
            String data = _service.DesbloquearUsuario(nombreUsuario);
            return Ok(new
            {
                data
            });
        }

    }
}

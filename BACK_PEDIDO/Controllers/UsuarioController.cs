using AutoMapper;
using BACK_PEDIDO.Models;
using BusinesssLayer;
using DataLayer.COMMON;
using EntityLayer.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACK_PEDIDO.Controllers
{

    [ApiController]
    [Route(Common.generalRoute)]
    public class UsuarioController : Controller
    {
        Response response = new ();
        ResponseType rt = new();
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        
        [HttpGet]
        [Route(Common.getAPIObtenerUsuariosBloqueados)]
        //[Authorize]
        public ActionResult<IEnumerable<Response>> obtenerUsuariosBloqueados() {
            IEnumerable<UsuarioBlockDTO> data = _service.obtenerTodosUsuarios();

            response.data = response.setData(data);
            response.Message = Common.msjLoginValido;
            response.Code = ResponseType.Success;

            return Ok(new { 
                response
            });
        }

        [HttpPost]
        [Route(Common.getAPIBuscarUsuarioBloqueado)]
        public ActionResult<UsuarioBlockDTO> buscarUsuarioBloqueado(string nombreUsuario) {
            UsuarioBlockDTO data = _service.buscarUsuarioBloqueado(nombreUsuario);
            return Ok(new { 
                data
            });
        }

        [HttpPut]
        [Route(Common.getAPIDesbloquearUsuario)] 
        public ActionResult<String> DesbloquearUsuario(string nombreUsuario) {
            String data = _service.DesbloquearUsuario(nombreUsuario);
            return Ok(new { 
                data
            });
        }

        [HttpPost]
        [Route(Common.getAPIValidarLogin)]
        public ActionResult<Response> ValidarLogin([FromBody] UsuarioLoginDTO usuario) {
            

            UsuarioLoginDTO data = _service.validarLogin(usuario);

            response.data = response.setData(data);
            response.Message = Common.msjLoginValido;
            response.Code = ResponseType.Success;
            return Ok(
                   response
            );  
        }
    }
}

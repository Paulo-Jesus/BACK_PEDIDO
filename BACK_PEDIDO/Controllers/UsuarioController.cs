using AutoMapper;
using BACK_PEDIDO.Models;
using BusinesssLayer;
using DataLayer.COMMON;
using EntityLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BACK_PEDIDO.Controllers
{

    [ApiController]
    [Route(Common.generalRoute)]
    public class UsuarioController : Controller
    {

        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route(Common.getAPIObtenerUsuariosBloqueados)]
        public ActionResult<IEnumerable<Usuario>> obtenerUsuariosBloqueados() {
            IEnumerable<UsuarioBlockDTO> data = _service.obtenerTodosUsuarios();
            return Ok(new { 
                data
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
    }
}

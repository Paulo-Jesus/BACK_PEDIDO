using BusinessLayer.Services.Seguridad.Usuarios;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;
        private Response response = new();

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [Route("Usuarios/Obtener")]
        [HttpGet]
        public async Task<IActionResult> UsuariosObtener()
        {
            response = await _usuariosService.UsuariosObtener();

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route("Usuarios/Agregar")]
        [HttpPost]
        public async Task<IActionResult> UsuariosAgregar([FromBody] UsuarioDTO usuarioDTO)
        {
            response = await _usuariosService.UsuariosAgregar(usuarioDTO);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route("Usuarios/Buscar")]
        [HttpGet]
        public async Task<IActionResult> UsuariosBuscar(string? Cedula, string? Nombre, int? IdEmpresa)
        {
            response = await _usuariosService.UsuariosBuscar(Cedula, Nombre, IdEmpresa);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route("Usuarios/Editar")]
        [HttpPut]
        public async Task<IActionResult> UsuariosEditar(UsuarioDTO usuarioDTO)
        {
            response = await _usuariosService.UsuariosEditar(usuarioDTO);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }
    }
}

using BusinessLayer.Services.Interfaces;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        private readonly ISeguridadService _seguridadService;
        private Response response = new();

        public SeguridadController(ISeguridadService seguridadService) 
        {
            _seguridadService = seguridadService;
        }

        [Route("Usuarios/Obtener")]
        [HttpGet]
        public async Task<IActionResult> UsuariosObtener()
        {
            response = await _seguridadService.UsuariosObtener();

            if(response.Code == ResponseType.Error)
                return BadRequest(response);
            
            return Ok(response);
            
        }

        [Route("Usuarios/Agregar")]
        [HttpPost]
        public async Task<IActionResult> UsuariosAgregar([FromBody] UsuarioDTO usuarioDTO)
        {
            response = await _seguridadService.UsuariosAgregar(usuarioDTO);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }
        
        [Route("Usuarios/Buscar")]
        [HttpGet]
        public async Task<IActionResult> UsuariosBuscar(string? Cedula, string? Nombre, int? IdEmpresa)
        {
            response = await _seguridadService.UsuariosBuscar(Cedula, Nombre, IdEmpresa);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route("Usuarios/Editar")]
        [HttpPut]
        public async Task<IActionResult> UsuariosEditar(UsuarioDTO usuarioDTO)
        {
            response = await _seguridadService.UsuariosEditar(usuarioDTO);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }
    }
}

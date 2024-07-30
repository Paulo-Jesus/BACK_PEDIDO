using BusinessLayer.Services.Seguridad.Usuarios;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;
using API.Common;

namespace API.Controllers.Seguridad
{
    [Route(APIRoutes.Route)]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;
        private Response response = new();

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [Route(APIRoutes.ObtenerTodos)]
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            response = await _usuariosService.ObtenerTodos();

            if (response.Code == ResponseType.Error)    
                return BadRequest(response);

            return Ok(response);

        }        

        [Route(APIRoutes.Buscar)]
        [HttpGet]
        public async Task<IActionResult> Buscar(string? Cedula, string? Nombre, int? IdEmpresa)
        {
            response = await _usuariosService.Buscar(Cedula, Nombre, IdEmpresa);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route(APIRoutes.Agregar)]
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] usuarioDTOEditar usuarioDTO)
        {
            response = await _usuariosService.Agregar(usuarioDTO);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route(APIRoutes.Editar)]
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] usuarioDTOEditar usuarioDTO)
        {
            response = await _usuariosService.Editar(usuarioDTO);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route(APIRoutes.Eliminar)]
        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdUsuario)
        {
            response = await _usuariosService.Elminar(IdUsuario);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }
    }
}

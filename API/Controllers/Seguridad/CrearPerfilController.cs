using BusinessLayer.Services.Seguridad.CrearPerfil;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;
using API.Common;

namespace API.Controllers.Seguridad
{
    [Route(APIRoutes.Route)]
    [ApiController]
    public class CrearPerfilController : ControllerBase
    {
        private readonly ICrearPerfilService _crearPerfilService;
        private Response response = new();

        public CrearPerfilController(ICrearPerfilService crearPerfilService)
        {
            _crearPerfilService = crearPerfilService;
        }

        [Route(APIRoutes.AddRol)]
        [HttpPost]
        public async Task<IActionResult> AddRol(RolesDTO rol)
        {
            response = await _crearPerfilService.AddRol(rol);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route(APIRoutes.Editar)]
        [HttpPut]
        public async Task<IActionResult> Editar(RolesDTO rol)
        {
            response = await _crearPerfilService.Editar(rol);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route(APIRoutes.GetListEstados)]
        [HttpGet]
        public async Task<IActionResult> GetListEstados()
        {
            response = await _crearPerfilService.GetListEstados();

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }

        [Route(APIRoutes.GetListRoles)]
        [HttpGet]
        public async Task<IActionResult> GetListRoles()
        {
            response = await _crearPerfilService.GetListRoles();

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);
        }
    }
}

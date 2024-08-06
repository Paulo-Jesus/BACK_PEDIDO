using API.Common;
using BusinessLayer.Services.Seguridad.Rol;
using BusinessLayer.Services.Seguridad.Usuarios;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Seguridad

{

    [Route(APIRoutes.Route)]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;
        private Response response = new();

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [Route(APIRoutes.GetListRoles)]
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            response = await _rolService.Lista();

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }
    }
}

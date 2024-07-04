using BusinessLayer.Services.Parametros;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Parametros
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrosController : ControllerBase
    {
        private readonly IParametrosService _parametrosService;
        private Response response = new();

        public ParametrosController(IParametrosService parametrosService)
        {
            _parametrosService = parametrosService;
        }

        [Route("Guardar")]
        [HttpPost]
        public async Task<IActionResult> UsuariosAgregar([FromBody] EmpresaDTO empresaDTO)
        {
            response = await _parametrosService.Guardar(empresaDTO);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }
    }
}

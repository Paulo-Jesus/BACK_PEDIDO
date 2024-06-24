using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController(IEstadoService estadoService) : ControllerBase
    {
        private readonly IEstadoService _estadoService = estadoService;
        private Response response = new();

        [Route("ObtenerEstadosEF")]
        [HttpGet]
        public async Task<IActionResult> ObtenerEstadosEF()
        {
            try
            {
                response = await _estadoService.ObtenerEstadosEF();

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;

                return BadRequest(response);
            }

        }
    }
}

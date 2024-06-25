using BusinesssLayer.Services.Estados_Act_Inac;
using EntityLayer.Model;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {

        private readonly EstadoService _service;
        
        public EstadosController(EstadoService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Response estados = await _service.GetListEstados();
            return Ok(estados);
        }

    }
}

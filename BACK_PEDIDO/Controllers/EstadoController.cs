using BusinesssLayer.Services.Estados_Act_Inac;
using BusinesssLayer.Services.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACK_PEDIDO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {

        private readonly EstadoService _service;

        public EstadoController(EstadoService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var estados = await _service.GetListEstados();
            return Ok(estados);
        }


    }
}

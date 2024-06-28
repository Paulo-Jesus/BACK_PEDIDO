using BusinessLayer.Services.Pedidos.Pedidos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Pedidos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {

        private readonly PedidosService _service;

        public PedidosController(PedidosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pedidos = await _service.GetListPedidos();
            return Ok(pedidos);
        }


    }
}

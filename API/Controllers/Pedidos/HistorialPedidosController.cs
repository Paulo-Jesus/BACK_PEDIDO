using BusinessLayer.Services.Pedidos.HistorialPedidos;
using BusinessLayer.Services.Seguridad.Usuarios;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Pedidos
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialPedidosController : ControllerBase
    {
        private readonly IHistorialPedidosService _historialPedidosService;
        private Response response = new();

        public HistorialPedidosController(IHistorialPedidosService historialPedidosService)
        {
            _historialPedidosService = historialPedidosService;
        }

        [Route("ObtenerPedidos")]
        [HttpGet]
        public async Task<IActionResult> ObtenerPedidos()
        {
            response = await _historialPedidosService.GetListPedidos();

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }
    }
}

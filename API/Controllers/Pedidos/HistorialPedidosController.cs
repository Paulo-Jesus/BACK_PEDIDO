using BusinessLayer.Services.Pedidos.HistorialPedidos;
using BusinessLayer.Services.Seguridad.Usuarios;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Common;

namespace API.Controllers.Pedidos
{
    [Route(APIRoutes.Route)]
    [ApiController]
    public class HistorialPedidosController : ControllerBase
    {
        private readonly IHistorialPedidosService _historialPedidosService;
        private Response response = new();

        public HistorialPedidosController(IHistorialPedidosService historialPedidosService)
        {
            _historialPedidosService = historialPedidosService;
        }

        [Route(APIRoutes.ObtenerPedidos)]
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

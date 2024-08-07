using API.Common;
using BusinessLayer.Services.Pedidos.InsertarPedido;
using BusinessLayer.Services.Pedidos.Menu;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Pedidos.Pedidos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private Response response = new();

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [Route(APIRoutes.InsertarPedido)]
        [HttpPut]
        public async Task<IActionResult> InsertarPedido([FromBody] PedidosInsertarDto pedido)
        {
            response = await _pedidoService.InsertarPedido(pedido);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

    }
}

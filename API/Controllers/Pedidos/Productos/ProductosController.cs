using BusinessLayer.Services.Pedidos.Productos;
using BusinessLayer.Services.Seguridad.Usuarios;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;
using API.Common;

namespace API.Controllers.Pedidos.Productos
{
    [Route(APIRoutes.Route)]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private Response response = new();
        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [Route(APIRoutes.ObtenerProductos)]
        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(int IdProveedor)
        {
            response = await _productoService.ObtenerProductos(IdProveedor);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route(APIRoutes.IngresarProducto)]
        [HttpPost]
        public async Task<IActionResult> IngresarProducto([FromBody] ProductoDTO productoDTO)
        {
            response = await _productoService.IngresarProducto(productoDTO);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route(APIRoutes.ActualizarProducto)]
        [HttpPut]
        public async Task<IActionResult> ActualizarProducto([FromBody] ProductoDTO productoDTO)
        {
            response = await _productoService.ActualizarProducto(productoDTO);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route(APIRoutes.EstadoProducto)]
        [HttpPut]
        public async Task<IActionResult> EstadoProducto(int productoId, int estado)
        {
            response = await _productoService.EstadoProducto(productoId, estado);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }
    }
}

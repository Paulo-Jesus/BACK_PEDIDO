using API.Common;
using BusinessLayer.Services.Pedidos.Menu;
using BusinessLayer.Services.Pedidos.Productos;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Pedidos.Menu
{
    [Route(APIRoutes.Route)]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private Response response = new();

        public MenuController(IMenuService menuService) 
        {
            _menuService = menuService;
        }

        [Route(APIRoutes.MenuIngresar)]
        [HttpPost]
        public async Task<IActionResult> IngresarMenu(int idProveedor, string HoraInicio, string HoraFin, int[] IdProductos)
        {
            response = await _menuService.RegistrarMenu(idProveedor, HoraInicio, HoraFin, IdProductos);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route(APIRoutes.ActualizarMenu)]
        [HttpPut]
        public async Task<IActionResult> ActualizarMenu(int IdProveedor, string HoraInicio, string HoraFin, int[] IdProductos)
        {
            response = await _menuService.ActualizarMenu(IdProveedor, HoraInicio, HoraFin, IdProductos);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route(APIRoutes.ConsultaTieneMenu)]
        [HttpGet]
        public async Task<IActionResult> TieneMenu(int IdProveedor)
        {
            response = await _menuService.TieneMenu(IdProveedor);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }

        [Route(APIRoutes.DatosMenu)]
        [HttpGet]
        public async Task<IActionResult> DatosMenu(int IdProveedor)
        {
            response = await _menuService.DatosMenuCompleto(IdProveedor);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }
    }
}

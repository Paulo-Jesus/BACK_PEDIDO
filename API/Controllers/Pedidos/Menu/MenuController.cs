using BusinessLayer.Services.Pedidos.Menu;
using BusinessLayer.Services.Pedidos.Productos;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Pedidos.Menu
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private Response response = new();

        public MenuController(IMenuService menuService) 
        {
            _menuService = menuService;
        }

        [Route("Menu/Ingresar")]
        [HttpPost]
        public async Task<IActionResult> IngresarMenu(int idProveedor, string HoraInicio, string HoraFin, int[] IdProductos)
        {
            response = await _menuService.RegistrarMenu(idProveedor, HoraInicio, HoraFin, IdProductos);

            if (response.Code == ResponseType.Error)
                return BadRequest(response);

            return Ok(response);

        }
    }
}

using BusinessLayer.Services;
using Entity = EntityLayer.Models.Entities;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Models.DTO;
using BusinessLayer.Services.Proveedor;

namespace API.Controllers.Pedidos.Proveedor
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;
        Response response = new();

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        [Route(API.Common.APIRoutes.APIObtenerProveedores)]
        public async Task<ActionResult<Response>> GetRestaurantes()
        {
            Response data = await _proveedorService.GetRestaurantes();
            return Ok(data);
        }

        [HttpPost]
        [Route(API.Common.APIRoutes.APIRegistrarProveedores)]
        public async Task<ActionResult<Response>> GetRestaurantes([FromBody] ProveedorDTO restaurante)
        {
            Response data = await _proveedorService.registrar(restaurante);
            return Ok(new
            {
                data
            });
        }
    }
}

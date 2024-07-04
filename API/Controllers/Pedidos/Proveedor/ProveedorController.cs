using BusinessLayer.Services;
using Entity = EntityLayer.Models.Entities;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Models.DTO;
using BusinessLayer.Services.Proveedor;

namespace API.Controllers.Pedidos.Proveedor
{
    public class ProveedorController : ControllerBase
    {
        Response response = new();
        ResponseType rt = new();
        private readonly ProveedorService _serviceR;

        public ProveedorController(ProveedorService service)
        {
            _serviceR = service;
        }

        [HttpGet]
        [Route(API.Common.APIRoutes.APIObtenerProveedores)]
        public async Task<ActionResult<IEnumerable<Entity.Proveedor>>> GetRestaurantes()
        {

            IEnumerable<ProveedorDTO> data = await _serviceR.GetRestaurantes();

            response.Data = data;
            response.Message = DataLayer.Common.DLMessages.ListadoUsuarios;
            response.Code = ResponseType.Success;

            return Ok(response);
        }

        [HttpPost]
        [Route(API.Common.APIRoutes.APIRegistrarProveedores)]
        public async Task<ActionResult<Response>> GetRestaurantes([FromBody] ProveedorDTO restaurante)
        {
            Response data = await _serviceR.registrar(restaurante);
            return Ok(new
            {
                data
            });
        }
    }
}

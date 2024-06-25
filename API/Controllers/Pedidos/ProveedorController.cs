using Microsoft.AspNetCore.Mvc;
using EntityLayer.Responses;
using BusinessLayer.Services.Seguridad.DesbloquearCuenta;
using EntitiLayer.Models.Entities;

namespace API.Controllers.Pedidos
{
    public class ProveedorController : Controller
    {
        Response response = new();
        ResponseType rt = new();
        private readonly ProveedorService _serviceR;

        public ProveedorController(ProveedorService service)
        {
            _serviceR = service;
        }

        [HttpGet]
        [Route("/getRestaurantes")]
        public ActionResult<IEnumerable<Proveedor>> GetRestaurantes()
        {

            IEnumerable<Proveedor> data = _serviceR.GetRestaurantes();

            response.Data = data;
            response.Message = DataLayer.Common.DLMessages.ListadoUsuarios;
            response.Code = ResponseType.Success;

            return Ok(response);
        }

        [HttpPost]
        [Route("/registrar")]
        public ActionResult<Response> GetRestaurantes([FromBody] Proveedor restaurante)
        {
            Response data = _serviceR.registrar(restaurante);
            return Ok(new
            {
                data
            });
        }
    }
}

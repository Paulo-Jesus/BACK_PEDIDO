using BusinesssLayer;
using DataLayer.COMMON;
using EntityLayer.Models;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BACK_PEDIDO.Controllers
{
    [ApiController]
    public class RestauranteController : Controller      
    {
        Response response = new();
        ResponseType rt = new();
        private readonly RestauranteService _serviceR;

        public RestauranteController(RestauranteService service)
        {
            _serviceR = service;
        }

        [HttpGet]
        [Route("/getRestaurantes")]
        public ActionResult<IEnumerable<Restaurante>> GetRestaurantes() {
            
            IEnumerable<Restaurante> data = _serviceR.GetRestaurantes();

            response.data = response.setData(data);
            response.Message = Common.msjExito;
            response.Code = ResponseType.Success;

            return Ok(response);
        }

        [HttpPost]
        [Route("/registrar")]
        public ActionResult<Response> GetRestaurantes([FromBody] Restaurante restaurante)
        {
            Response data = _serviceR.registrar(restaurante);
            return Ok(new { 
                data
            });
        }

    }
}

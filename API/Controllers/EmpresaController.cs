using BusinessLayer.Services.Interfaces;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController(IEmpresaService empresaService) : ControllerBase
    {
        private readonly IEmpresaService _empresaService = empresaService;
        private Response response = new();

        [Route("ObtenerEmpresasEF")]
        [HttpGet]
        public async Task<IActionResult> ObtenerEstadosEF()
        {
            try
            {
                response = await _empresaService.ObtenerEmpresasEF();

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;

                return BadRequest(response);
            }
        }

        [Route("RegistrarEmpresaADO")]
        [HttpPost]
        public async Task<IActionResult> RegistrarEmpresaADO([FromBody] EmpresaDTO empresaDTO)
        {
            try
            {
                response = await _empresaService.RegistrarEmpresaADO(empresaDTO);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;

                return BadRequest(response);
            }
        }
    }
}

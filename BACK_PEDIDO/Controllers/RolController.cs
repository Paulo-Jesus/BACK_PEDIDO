using BACK_PEDIDO.Models;
using BusinesssLayer.Services.Roles;
using EntityLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BACK_PEDIDO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

       private readonly RolService _service;

        public RoleController(RolService service) { 
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _service.GetListRoles();
            return Ok(roles);
        }



        [HttpPost]
        public async Task<IActionResult> Post(Rol rol)
        {
                var roles=_service.AddRol(rol);
                return Ok(roles);
        }



    }
}

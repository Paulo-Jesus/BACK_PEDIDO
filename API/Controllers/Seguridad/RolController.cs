
using BusinesssLayer.Services.Roles;
using EntityLayer.Model;
using EntityLayer.Responses;
using Microsoft.AspNetCore.Mvc;

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
            Response roles = await _service.GetListRoles();
            return Ok(roles);
        }



        [HttpPost]
        public IActionResult Post(RolesDTO rol)
        {
            Response roles = _service.AddRol(rol);
            return Ok(roles);
        }

        [HttpPut]
        public IActionResult Put(RolesDTO rol)
        {
            Response rolMod = _service.updateRol(rol);
            return Ok(rolMod);
        }



    }
}

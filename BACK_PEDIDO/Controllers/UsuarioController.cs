using BACK_PEDIDO.Models;
using Microsoft.AspNetCore.Mvc;

namespace BACK_PEDIDO.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsuarioController : Controller
    {
        private readonly BdPedidosContext _context;
        public UsuarioController(BdPedidosContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Usuario> obtenerTodos() {
            return Ok(_service.obtenerTodos());
        }
    }
}

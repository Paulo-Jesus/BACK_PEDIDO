using BACK_PEDIDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<Usuario> obtenerTodosUsuarios();
    }
}

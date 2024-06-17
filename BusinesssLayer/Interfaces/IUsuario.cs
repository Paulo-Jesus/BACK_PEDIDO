using BACK_PEDIDO.Models;
using EntityLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<UsuarioBlockDTO> obtenerTodosUsuarios();
    }
}

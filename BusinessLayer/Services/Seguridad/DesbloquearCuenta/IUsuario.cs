using EntityLayer.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.DesbloquearCuenta
{
    public interface IUsuario
    {
        IEnumerable<UsuarioBlockDTO> obtenerTodosUsuarios();
        UsuarioBlockDTO buscarUsuarioBloqueado(string nombreUsuario);
        String DesbloquearUsuario(string nombreUsuario);

    }
}

using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.DesbloquearCuenta
{
    public interface IUsuario
    {
        Task<Response> obtenerTodosUsuarios();
        Task<Response> buscarUsuarioBloqueado(string nombreUsuario);
        Task<Response> DesbloquearUsuario(string nombreUsuario);

    }
}

using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Seguridad.DesbloquearCuenta
{
    public interface IUsuarioDcRepository
    {
        Task<Response> obtenerTodosUsuarios();
        Task<Response> buscarUsuarioBloqueado(string correo);
        Task<Response> DesbloquearUsuario(string correo);
    }
}

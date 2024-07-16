using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.DesbloquearCuenta
{
    public interface IUsuarioDcService
    {
        Task<Response> obtenerTodosUsuarios();
        Task<Response> buscarUsuarioBloqueado(string Correo);
        Task<Response> DesbloquearUsuario(string Correo);

    }
}

using EntitiLayer.Models.Entities;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.DesbloquearCuenta
{
    public interface IProveedor
    {
        IEnumerable<Proveedor> GetRestaurantes();
        Response registrar(Proveedor proveedor);
    }
}

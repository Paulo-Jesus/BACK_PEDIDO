using EntityLayer.Models;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer.Interfaces
{
    public interface IRestaurantes
    {
        IEnumerable<Restaurante> GetRestaurantes();
        Response registrar(Restaurante restaurante);
    }
}

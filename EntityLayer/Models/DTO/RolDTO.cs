using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.DTO
{
  public class RolDTO
    {
        public int IdRol {  get; set; }
        public string? Nombre { get; set; }

        public int IdEstado { get; set; }
        public RolDTO(int idRol,string nombre, int idEstado)
        {
            IdRol = idRol;
            Nombre = nombre;
            IdEstado = idEstado;    

        }
    }
}

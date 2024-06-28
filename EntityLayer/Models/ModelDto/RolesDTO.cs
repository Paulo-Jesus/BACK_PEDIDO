using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Model
{
    public class RolesDTO
    {
        public RolesDTO(){ }
        public string Nombre { get; set; }
        public int Estado { get; set; }


        public RolesDTO(string nombre, int estado)
        {
            Nombre = nombre;
            Estado = estado;
        }
    }
}

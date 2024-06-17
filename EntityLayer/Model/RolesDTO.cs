using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Model
{
    public class RolesDTO
    {
        public RolesDTO(string nombre)
        {
            Nombre = nombre;
        }
        public string Nombre { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Model
{
    public class EstadoDTO
    {
   
        public string Nombre { get; set; }

        public EstadoDTO(string nombre)
        {

            Nombre = nombre;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.DTO
{
    public class UsuarioBlockDTO
    {
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public int idEstado { get; set; }
        public string NombreEstado { get; set; }
        public string NombreRol { get; set; }
    }
}

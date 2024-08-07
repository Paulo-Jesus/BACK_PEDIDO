using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.DTO
{
    public class UsuarioBlockDTO
    {
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string NombreRol { get; set; } = null!;
        public string NombreEstado { get; set; } = null!;

    }
}

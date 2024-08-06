using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.DTO
{
    public class CuentaDTO
    {
        public int IdCuenta { get; set; }
        public string? NombreCompleto { get; set; }
        public string Cedula { get; set; } = null!;
        public string? Correo { get; set; }
        public int? IdRol { get; set; }
        public bool? IdEstado { get; set; }
      



    }
}

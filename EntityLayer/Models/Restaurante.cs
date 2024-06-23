using BACK_PEDIDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class Restaurante
    {
        public int IdRestaurante { get; set; }
        public string? RUC { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Username { get; set; }
        public string? Logotipo { get; set; }
        public string? Contrasena { get; set; }
        public int IdRol { get; set; }
        public int IdEstado { get; set; }

        public virtual Rol? RolIdRolNavigation { get; set; }
        public virtual Estado? EstadoIdEstadoNavigation { get; set; }

    }
}

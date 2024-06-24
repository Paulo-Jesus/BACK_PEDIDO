using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Model
{
    public class PedidosDto
    {
        public string? FechaPedido { get; set; }
        public string? NombreUsuario { get; set;}
        public string? NombrePedido { get; set;}
        public decimal PrecioProducto { get; set;}
        public int Cantidad { get; set;}
    }
}

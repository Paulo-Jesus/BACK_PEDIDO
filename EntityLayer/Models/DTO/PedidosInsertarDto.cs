using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.DTO
{
    public class PedidosInsertarDto
    {
        public int? idUsuario { get; set; }
        public int? idProveedor { get; set; }
        public int? idProducto { get; set; }
    }
}

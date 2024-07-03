using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public double Precio { get; set; }
        public string? Categoria { get; set; }
        public string? ImagenBase64 { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
        public int IdEstado { get; set; }
    }
}

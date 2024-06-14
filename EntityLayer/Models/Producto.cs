using System;
using System.Collections.Generic;

namespace BACK_PEDIDO.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public byte[]? Imagen { get; set; }

    public int? CategoriaIdCategoria { get; set; }

    public int? EstadoIdEstado { get; set; }

    public virtual Categorium? CategoriaIdCategoriaNavigation { get; set; }

    public virtual Estado? EstadoIdEstadoNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}

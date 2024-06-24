using System;
using System.Collections.Generic;

namespace EntityLayer.Models.Entities;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public byte[]? Imagen { get; set; }

    public int IdCategoria { get; set; }

    public int IdRestaurante { get; set; }

    public int IdEstado { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Proovedor IdRestauranteNavigation { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}

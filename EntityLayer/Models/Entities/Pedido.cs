using System;
using System.Collections.Generic;

namespace EntitiLayer.Models.Entities;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public DateTime? FechaPedido { get; set; }

    public int IdUsuario { get; set; }

    public int IdProveedor { get; set; }

    public int IdProducto { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

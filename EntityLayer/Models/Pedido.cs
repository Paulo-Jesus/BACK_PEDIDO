using System;
using System.Collections.Generic;

namespace BACK_PEDIDO.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? UsuarioIdUsuario { get; set; }

    public int? ProductoIdProducto { get; set; }

    public int? EstadoIdEstado { get; set; }

    public virtual Estado? EstadoIdEstadoNavigation { get; set; }

    public virtual Producto? ProductoIdProductoNavigation { get; set; }

    public virtual Usuario? UsuarioIdUsuarioNavigation { get; set; }
}

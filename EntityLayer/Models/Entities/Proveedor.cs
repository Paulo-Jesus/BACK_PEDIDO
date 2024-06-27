using System;
using System.Collections.Generic;

namespace EntityLayer.Models.Entities;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string Ruc { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public byte[]? Logotipo { get; set; }

    public int IdCuenta { get; set; }

    public int IdEstado { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

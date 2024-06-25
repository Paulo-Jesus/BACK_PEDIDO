using System;
using System.Collections.Generic;

namespace EntitiLayer.Models.Entities;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string Ruc { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int IdRol { get; set; }

    public int IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

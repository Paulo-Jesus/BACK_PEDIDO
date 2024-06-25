using System;
using System.Collections.Generic;

namespace EntitiLayer.Models.Entities;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Proveedor> Proveedors { get; set; } = new List<Proveedor>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

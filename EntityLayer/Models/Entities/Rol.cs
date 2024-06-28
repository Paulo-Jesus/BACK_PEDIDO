using System;
using System.Collections.Generic;

namespace EntityLayer.Models.Entities;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdEstado { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual Estado IdEstadoNavigation { get; set; } = null!;
}

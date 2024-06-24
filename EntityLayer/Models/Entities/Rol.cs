using System;
using System.Collections.Generic;

namespace EntityLayer.Models.Entities;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Proovedor> Proovedors { get; set; } = new List<Proovedor>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

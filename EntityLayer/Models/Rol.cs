using System;
using System.Collections.Generic;

namespace BACK_PEDIDO.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public int? EstadoIdEstado { get; set; }

    public virtual Estado? EstadoIdEstadoNavigation { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

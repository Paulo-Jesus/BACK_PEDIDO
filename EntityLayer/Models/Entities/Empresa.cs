using System;
using System.Collections.Generic;

namespace EntitiLayer.Models.Entities;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string Ruc { get; set; } = null!;

    public string RazonSocial { get; set; } = null!;

    public int IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

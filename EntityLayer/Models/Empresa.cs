using System;
using System.Collections.Generic;

namespace BACK_PEDIDO.Models;

public partial class Empresa
{
    public int IdEmpresa { get; set; }

    public string Ruc { get; set; } = null!;

    public string RazonSocial { get; set; } = null!;

    public int? EstadoIdEstado { get; set; }

    public byte[]? Logo { get; set; }

    public virtual Estado? EstadoIdEstadoNavigation { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

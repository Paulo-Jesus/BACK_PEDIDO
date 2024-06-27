using System;
using System.Collections.Generic;

namespace EntityLayer.Models.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int IdCuenta { get; set; }

    public int IdEmpresa { get; set; }

    public int IdEstado { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}

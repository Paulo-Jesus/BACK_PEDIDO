using System;
using System.Collections.Generic;

namespace BACK_PEDIDO.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public int? EmpresaIdEmpresa { get; set; }

    public int? RolIdRol { get; set; }

    public int? EstadoIdEstado { get; set; }

    public virtual Empresa? EmpresaIdEmpresaNavigation { get; set; }

    public virtual Estado? EstadoIdEstadoNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Rol? RolIdRolNavigation { get; set; }
}

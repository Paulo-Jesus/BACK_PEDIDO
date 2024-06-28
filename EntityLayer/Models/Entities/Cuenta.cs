using System;
using System.Collections.Generic;

namespace EntityLayer.Models.Entities;

public partial class Cuenta
{
    public int IdCuenta { get; set; }

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int IdRol { get; set; }

    public int IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Proveedor> Proveedors { get; set; } = new List<Proveedor>();
    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

using System;
using System.Collections.Generic;

namespace EntityLayer.Models.Entities;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Empresa> Empresas { get; set; } = new List<Empresa>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual ICollection<Proovedor> Proovedors { get; set; } = new List<Proovedor>();

    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

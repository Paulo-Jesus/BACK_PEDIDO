using System;
using System.Collections.Generic;

namespace BACK_PEDIDO.Models;

public class Estado
{
    public int IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public  ICollection<Categorium> Categoria { get; set; } = new List<Categorium>();

    public  ICollection<Empresa> Empresas { get; set; } = new List<Empresa>();

    public  ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public  ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public  ICollection<Rol> Rols { get; set; } = new List<Rol>();

    public
        ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

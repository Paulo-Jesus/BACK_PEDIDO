﻿using System;
using System.Collections.Generic;

namespace EntitiLayer.Models.Entities;

public partial class MenuDetalle
{
    public int IdMenuDetalle { get; set; }

    public int IdProducto { get; set; }
    public int IdMenu { get; set; }

    public virtual Menu IdMenuNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}

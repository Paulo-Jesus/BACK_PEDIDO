using System;
using System.Collections.Generic;

namespace EntitiLayer.Models.Entities;

public partial class Menu
{
    public int IdMenu { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int IdProveedor { get; set; }

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
    public virtual ICollection<MenuDetalle> MenuDetalles { get; set; } = new List<MenuDetalle>();
}

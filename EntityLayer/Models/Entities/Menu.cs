using System;
using System.Collections.Generic;

namespace EntitiLayer.Models.Entities;

public partial class Menu
{
    public int IdMenu { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int IdProveedor { get; set; }

    public int IdMenuDetalle { get; set; }

    public virtual MenuDetalle IdMenuDetalleNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}

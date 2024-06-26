﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.DTO
{
    public class usuarioDTOEditar
    {
        public int IdUsuario { get; set; }

        public string Cedula { get; set; } = null!;

        public string Nombre { get; set; } = null!; 

        public string Correo { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public int IdRol { get; set; }

        public int IdEmpresa { get; set; }

        public int IdEstado { get; set; }

        public int IdCuenta { get; set; }
    }
}

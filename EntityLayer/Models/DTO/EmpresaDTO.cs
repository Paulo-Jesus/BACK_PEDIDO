using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.DTO
{
    public class EmpresaDTO
    {
        public int IdEmpresa { get; set; }

        public string Ruc { get; set; } = null!;

        public string RazonSocial { get; set; } = null!;

        public int IdEstado { get; set; }
    }
}

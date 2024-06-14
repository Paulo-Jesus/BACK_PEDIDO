using AutoMapper;
using BACK_PEDIDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Model
{
    public class EstadoProfile : Profile
    {
       EstadoProfile() {
            CreateMap<Estado, EstadoDTO>();
            CreateMap<EstadoDTO, Estado>();
       }
    }
}

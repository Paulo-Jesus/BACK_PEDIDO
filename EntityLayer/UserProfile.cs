using AutoMapper;
using BACK_PEDIDO.Models;
using EntityLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<Usuario, UsuarioBlockDTO>();
            CreateMap<UsuarioBlockDTO, Usuario>();
        }
    }
}

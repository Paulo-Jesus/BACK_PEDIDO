﻿using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Seguridad.CrearPerfil
{
    public interface ICrearPerfilService
    {
        public Task<Response> GetListEstados();

        public Task<Response> GetListRoles();

<<<<<<< Updated upstream
        public Task<Response> AddRol(RolesDTO rol);

        public Task<Response> Editar(RolesDTO rolDTO);
=======
        public Task<Response> AddRol(RolDTO rol);

        public Task<Response> Editar(RolDTO rolDTO);
>>>>>>> Stashed changes
    }
}

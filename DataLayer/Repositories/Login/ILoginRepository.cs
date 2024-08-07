﻿using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Login
{
    public interface ILoginRepository
    {
        public Task<Response> IniciarSesion(LoginDTO request);

        public Task<Response> IniciarSesionGoogle(LoginDTO request);

        public Task<Response> GenerarContrasena(string Correo);

        public Task<Response> ComprobarToken(string tokenCuerpo);

        public Task<Response> RestablecerContrasena(string tokenCuerpo, string claveTemporal, string claveNueva);

        public Task<Response> RecuperarContrasena(string Correo);
    }
}

using DataLayer.Repositories.Login;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using System.Net.Mail;
using System.Net;
using BusinessLayer.Common;
using EntityLayer.Models.Entities;

namespace BusinessLayer.Services.Login
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepository _loginRepository;
        private Response response = new();
        private SmtpClient _smtpClient;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<Response> IniciarSesion(LoginDTO request)
        {
            response = await _loginRepository.IniciarSesion(request);
            return response;
        }

        public async Task<Response> IniciarSesionGoogle(LoginDTO request)
        {
            response = await _loginRepository.IniciarSesionGoogle(request);
            return response;
        }

        public async Task<Response> GenerarContrasena(string Correo)
        {
            try
            {
                response = await _loginRepository.GenerarContrasena(Correo);

                // Configura el cliente SMTP con la configuración de tu servidor de correo
                _smtpClient = new SmtpClient(BLVariable.ClienteSMTP)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(BLVariable.CorreoSMTP, BLVariable.ContraseñaSMTP),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new()
                {
                    From = new MailAddress(BLVariable.CorreoSMTP),
                    Subject = BLVariable.Empresa,

                    //AGREGAR EL CUERPO DEL MENSAJE
                    Body = response.Data!.ToString(),

                    IsBodyHtml = true,
                };

                mailMessage.To.Add(Correo);

                await _smtpClient.SendMailAsync(mailMessage);

                response.Code = ResponseType.Success;
                response.Message = BLVariable.CorreoEnviadoCorrectamente;
                response.Data = null;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = null;
            }
            finally
            {
                _smtpClient.Dispose();
            }

            return response;
        }

        public async Task<Response> ComprobarToken(string tokenCuerpo)
        {
            response = await _loginRepository.ComprobarToken(tokenCuerpo);
            return response;
        }

        public async Task<Response> RestablecerContrasena(string tokenCuerpo, string claveTemporal, string claveNueva) 
        {
            response = await _loginRepository.RestablecerContrasena(tokenCuerpo, claveTemporal, claveNueva);
            return response;
        }
    }
}

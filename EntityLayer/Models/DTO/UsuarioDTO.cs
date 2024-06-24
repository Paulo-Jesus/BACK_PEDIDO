namespace EntityLayer.Models.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string Cedula { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Username { get; set; } = null!;

        public int IdRol { get; set; }

        public int IdEmpresa { get; set; }

        public int IdEstado { get; set; }
    }
}

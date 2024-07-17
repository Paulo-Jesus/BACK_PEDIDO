namespace EntityLayer.Models.DTO
{
    public class ProveedorDTO
    {
        public int IdProveedor {get;set;}
        public string Ruc { get;set;} = string.Empty;
        public string Nombre { get;set;} = string.Empty;
        public string Correo { get;set;} = string.Empty;
        public string Telefono { get;set;} = string.Empty;
        public string Direccion { get;set;} = string.Empty;
        public string Logotipo { get;set;} = string.Empty;
        public string Contrasena { get;set;} = string.Empty;
        public int IdRol { get;set;}
        public int IdEstado { get;set;}
    }
}

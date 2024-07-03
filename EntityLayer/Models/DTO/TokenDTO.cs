namespace EntityLayer.Models.DTO
{
    public class TokenDTO
    {
        public string TokenCuerpo { get; set; } = null!;

        public int IdCuenta { get; set; }

        public DateTime FechaExpiracion { get; set; }
    }
}


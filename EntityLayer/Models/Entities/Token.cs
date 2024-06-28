namespace EntityLayer.Models.Entities
{
    public partial class Token
    {
        public int IdToken { get; set; }

        public string TokenCuerpo { get; set; } = null!;

        public int IdCuenta { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public virtual Cuenta IdCuentaNavigation { get; set; } = null!;
    }
}

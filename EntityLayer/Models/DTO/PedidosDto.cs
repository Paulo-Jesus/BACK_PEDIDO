namespace EntityLayer.Models.DTO
{
    public class PedidosDto
    {
        public string? FechaPedido { get; set; }
        public string? NombreUsuario { get; set; }
        public string? NombrePedido { get; set; }
        public decimal PrecioProducto { get; set; }
        public int Cantidad { get; set; }
    }
}

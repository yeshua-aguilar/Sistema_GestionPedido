using Microsoft.AspNetCore.Mvc;

namespace GestionPedidosAPIREST.DTO
{
    public class DetallePedidoDTO
    {
        public int CodigoPedido { get; set; }
        public int NumeroLinea { get; set; }
        public int? CodigoProducto { get; set; }
        public long? Cantidad { get; set; }
        public decimal? Total { get; set; }
    }

}

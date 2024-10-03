using System;
using System.Collections.Generic;

namespace GestionPedidosAPIREST.Models
{
    public partial class DetallePedido
    {
        public int CodigoPedido { get; set; }
        public int NumeroLinea { get; set; }
        public int? CodigoProducto { get; set; }
        public long? Cantidad { get; set; }
        public decimal? Total { get; set; }

        public virtual Producto? CodigoProductoNavigation { get; set; }
    }
}

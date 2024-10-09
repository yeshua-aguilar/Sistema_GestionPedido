using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestionPedidosAPIREST.Models
{
    public partial class DetallePedido
    {
        public int CodigoPedido { get; set; }
        public int NumeroLinea { get; set; }
        public int? CodigoProducto { get; set; }
        public long? Cantidad { get; set; }
        public decimal? Total { get; set; }

        [JsonIgnore]
        public virtual CabeceraPedido CodigoPedidoNavigation { get; set; } = null!;

        [JsonIgnore]
        public virtual Producto? CodigoProductoNavigation { get; set; }
    }
}

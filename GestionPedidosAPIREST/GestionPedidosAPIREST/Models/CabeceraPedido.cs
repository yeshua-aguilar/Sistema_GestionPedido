using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GestionPedidosAPIREST.Models
{
    public partial class CabeceraPedido
    {
        public int CodigoProducto { get; set; }
        public int? CodigoCliente { get; set; }
        public string? DireccionEntrega { get; set; }
        public decimal? Subtotal { get; set; }
        public long? CantidadTotal { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }

        [JsonIgnore]
        public virtual Cliente? CodigoClienteNavigation { get; set; }
    }
}

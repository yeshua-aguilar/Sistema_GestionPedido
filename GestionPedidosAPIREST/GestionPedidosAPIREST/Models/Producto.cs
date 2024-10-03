using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GestionPedidosAPIREST.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetallePedidos = new HashSet<DetallePedido>();
        }

        public int CodigoProducto { get; set; }
        public string? DescripcionProducto { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public string? Moneda { get; set; }

        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GestionPedidosAPIREST.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            CabeceraPedidos = new HashSet<CabeceraPedido>();
        }

        public int CodigoCliente { get; set; }
        public long? RucDni { get; set; }
        public string? RazonSocial { get; set; }
        public string? DireccionEntrega { get; set; }
        public string? DireccionFiscal { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? UsuarioRegistro { get; set; }

        [JsonIgnore]
        public virtual ICollection<CabeceraPedido> CabeceraPedidos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace GestionPedidosAPIREST.Models
{
    public partial class Usuario
    {
        public int CodigoUsuario { get; set; }
        public string? Usuario1 { get; set; }
        public string? Nombres { get; set; }
        public string? Clave { get; set; }
        public bool? EstadoSession { get; set; }
        public DateTime? UltimaFecha { get; set; }
        public string? FotoUsuario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GestionPedidosAPIREST.Models
{
    public partial class Usuario
    {
        public int CodigoUsuario { get; set; }
        public string? usuario { get; set; }
        public string? Nombres { get; set; }
        public string? Clave { get; set; }
        public bool? EstadoSession { get; set; }
        public DateTime? UltimaFecha { get; set; }
        public string? FotoUsuario { get; set; }
    }
}

using GestionPedidosAPIREST.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GestionPedidosAPIREST.Services
{
    public class UsuarioService
    {
        private readonly GestionPedidoContext _db;

        public UsuarioService(GestionPedidoContext db)
        {
            _db = db;
        }

        public Usuario GetUsuarioByUsername(string usuario)
        {
            return _db.Usuarios.FirstOrDefault(u => u.usuario == usuario);
        }

        public List<Usuario> GetUsuarios()
        {
            return _db.Usuarios.ToList();
        }

        public bool VerifyPassword(Usuario usuario, string password)
        {
            if (usuario == null || string.IsNullOrEmpty(password))
                return false;

            // Encriptar la contraseña con SHA256 antes de comparar
            string encryptedPassword = Encrypt(password);
            return encryptedPassword == usuario.Clave;
        }

        public string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "El parámetro password no puede ser nulo o vacío.");
            }

            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes); // Cambiado a Base64
            }
        }
    }

}

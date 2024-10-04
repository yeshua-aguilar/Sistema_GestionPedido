using GestionPedidosAPIREST.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace GestionPedidosAPIREST.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly GestionPedidoContext _dbcontext;

        public UsuarioController(GestionPedidoContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                usuarios = _dbcontext.Usuarios.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = usuarios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = usuarios });
            }
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear([FromBody] Usuario usuario)
        {
            try
            {
                // Encriptar la clave
                string encryptedPassword = Encrypt(usuario.Clave);

                usuario.Clave = encryptedPassword;

                _dbcontext.Usuarios.Add(usuario);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok", response = usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        private string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "El parámetro password no puede ser nulo o vacío.");
            }

            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        [HttpPut("Actualizar/{CodigoUsuario}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            var usuarioToUpdate = _dbcontext.Usuarios.Find(id);
            if (usuarioToUpdate == null)
            {
                return NotFound();
            }
            usuarioToUpdate.usuario = usuario.usuario;
            usuarioToUpdate.Nombres = usuario.Nombres;
            usuarioToUpdate.Clave = usuario.Clave;
            usuarioToUpdate.EstadoSession = usuario.EstadoSession;
            usuarioToUpdate.UltimaFecha = usuario.UltimaFecha;
            usuarioToUpdate.FotoUsuario = usuario.FotoUsuario;
            _dbcontext.SaveChanges();
            return Ok(usuarioToUpdate);
        }

        [HttpDelete]
        [Route("eliminar/{CodigoUsuario}")]
        public IActionResult Eliminar(int CodigoUsuario)
        {
            try
            {
                var usuario = _dbcontext.Usuarios.Find(CodigoUsuario);
                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }
                _dbcontext.Usuarios.Remove(usuario);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }



    }
}

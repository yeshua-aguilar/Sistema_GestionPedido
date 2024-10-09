using GestionPedidosAPIREST.Models;
using GestionPedidosAPIREST.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GestionPedidosAPIREST.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly JwtServices _jwtServices;
        private readonly UsuarioService _usuarioService;

        public LoginController(JwtServices jwtServices, UsuarioService usuarioService)
        {
            _jwtServices = jwtServices;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                Console.WriteLine($"Intento de login para usuario: {request.UsuarioName}");

                var usuario = _usuarioService.GetUsuarioByUsername(request.UsuarioName);
                if (usuario == null)
                {
                    Console.WriteLine($"Usuario no encontrado: {request.UsuarioName}");
                    return Unauthorized("Usuario no encontrado");
                }

                Console.WriteLine($"Usuario encontrado: {usuario.Usuario1}");

                if (!_usuarioService.VerifyPassword(usuario, request.Password))
                {
                    Console.WriteLine("Verificación de contraseña fallida");
                    return Unauthorized("Contraseña incorrecta");
                }

                Console.WriteLine("Contraseña verificada correctamente");

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Usuario1),
                    new Claim(ClaimTypes.NameIdentifier, usuario.CodigoUsuario.ToString()),
                    new Claim(ClaimTypes.GivenName, usuario.Nombres)
                };

                var token = _jwtServices.GenerateToken(claims);
                return Ok(new
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el proceso de login: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }


    }
    public class LoginRequest
    {
        public string UsuarioName { get; set; }
        public string Password { get; set; }
    }

}


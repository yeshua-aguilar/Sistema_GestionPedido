using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionPedidosAPIREST.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace GestionPedidosAPIREST.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly GestionPedidoContext _dbcontext;

        public ClientesController(GestionPedidoContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult Lista()
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                clientes = _dbcontext.Clientes.ToList();
                return Ok(new { mensaje = "ok", response = clientes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear([FromBody] Cliente cliente)
        {
            try
            {
                _dbcontext.Clientes.Add(cliente);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok", response = cliente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("detalle/{codigoCliente}")]
        public IActionResult Detalle(int codigoCliente)
        {
            try
            {
                var cliente = _dbcontext.Clientes.Find(codigoCliente);
                if (cliente == null)
                {
                    return NotFound(new { mensaje = "Cliente no encontrado" });
                }
                return Ok(new { mensaje = "ok", response = cliente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPatch]
        [Route("actualizar/{codigoCliente}")]
        public IActionResult Actualizar(int codigoCliente, [FromBody] Cliente cliente)
        {
            try
            {
                var clienteDB = _dbcontext.Clientes.Find(codigoCliente);
                if (clienteDB == null)
                {
                    return NotFound(new { mensaje = "Cliente no encontrado" });
                }
                clienteDB.RucDni = cliente.RucDni;
                clienteDB.RazonSocial = cliente.RazonSocial;
                clienteDB.DireccionEntrega = cliente.DireccionEntrega;
                clienteDB.DireccionFiscal = cliente.DireccionFiscal;
                clienteDB.FechaRegistro = cliente.FechaRegistro;
                clienteDB.UsuarioRegistro = cliente.UsuarioRegistro;

                _dbcontext.SaveChanges();
                return Ok(new { mensaje = "ok", response = clienteDB });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminar/{codigoCliente}")]
        public IActionResult Eliminar(int codigoCliente)
        {
            try
            {
                var cliente = _dbcontext.Clientes.Find(codigoCliente);
                if (cliente == null)
                {
                    return NotFound(new { mensaje = "Cliente no encontrado" });
                }
                _dbcontext.Clientes.Remove(cliente);
                _dbcontext.SaveChanges();
                return Ok(new { mensaje = "Cliente eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }
}

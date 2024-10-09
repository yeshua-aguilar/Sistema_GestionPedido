using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionPedidosAPIREST.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace GestionPedidosAPIREST.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CabederaPedidoController : ControllerBase
    {
        private readonly GestionPedidoContext _dbContext;

        public CabederaPedidoController(GestionPedidoContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener todos los pedidos
        [HttpGet]
        [Route("lista")]
        public IActionResult Lista()
        {
            try
            {
                var pedidos = _dbContext.CabeceraPedidos
                    .Include(cp => cp.DetallePedidos)
                    .ToList();

                return Ok(new { mensaje = "ok", response = pedidos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // Crear un nuevo pedido
        [HttpPost]
        [Route("crear")]
        public IActionResult Crear([FromBody] CabeceraPedido pedido)
        {
            if (pedido == null)
            {
                return BadRequest(new { mensaje = "El pedido no puede ser nulo." });
            }

            try
            {
                _dbContext.CabeceraPedidos.Add(pedido);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Pedido creado", response = pedido });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("detalle/{codigoPedido}")]
        public IActionResult Detalle(int codigoPedido)
        {
            try
            {
                var pedido = _dbContext.CabeceraPedidos.Find(codigoPedido);
                if (pedido == null)
                {
                    return NotFound(new { mensaje = "Pedido no encontrado" });
                }
                return Ok(new { mensaje = "ok", response = pedido });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPatch]
        [Route("actualizar/{codigoPedido}")]
        public IActionResult Actualizar(int codigoPedido, [FromBody] CabeceraPedido pedido)
        {
            if (pedido == null)
            {
                return BadRequest(new { mensaje = "El pedido no puede ser nulo." });
            }

            try
            {
                var pedidoDB = _dbContext.CabeceraPedidos.Find(codigoPedido);
                if (pedidoDB == null)
                {
                    return NotFound(new { mensaje = "Pedido no encontrado" });
                }

                // Actualiza las propiedades del pedido
                pedidoDB.CodigoCliente = pedido.CodigoCliente;
                pedidoDB.DireccionEntrega = pedido.DireccionEntrega;
                pedidoDB.Subtotal = pedido.Subtotal;
                pedidoDB.CantidadTotal = pedido.CantidadTotal;
                pedidoDB.Igv = pedido.Igv;
                pedidoDB.Total = pedido.Total;
                // Agrega aquí otras propiedades que necesiten ser actualizadas

                _dbContext.SaveChanges();
                return Ok(new { mensaje = "Pedido actualizado", response = pedidoDB });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminar/{codigoPedido}")]
        public IActionResult Eliminar(int codigoPedido)
        {
            try
            {
                var pedido = _dbContext.CabeceraPedidos.Find(codigoPedido);
                if (pedido == null)
                {
                    return NotFound(new { mensaje = "Pedido no encontrado" });
                }

                _dbContext.CabeceraPedidos.Remove(pedido);
                _dbContext.SaveChanges();
                return Ok(new { mensaje = "Pedido eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}

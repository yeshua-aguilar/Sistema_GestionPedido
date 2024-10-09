using GestionPedidosAPIREST.DTO;
using GestionPedidosAPIREST.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionPedidosAPIREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : Controller
    {
        private readonly GestionPedidoContext _dbContext;

        public DetallePedidoController(GestionPedidoContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener todos los detalles de pedidos
        [HttpGet]
        [Route("lista")]
        public IActionResult Lista()
        {
            try
            {
                var detalles = _dbContext.DetallePedidos.ToList();
                return Ok(new { mensaje = "ok", response = detalles });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // Crear un nuevo detalle de pedido
        [HttpPost]
        [Route("crear")]
        public IActionResult Crear([FromBody] DetallePedidoDTO detalleDto)
        {
            if (detalleDto == null)
            {
                return BadRequest(new { mensaje = "El detalle no puede ser nulo." });
            }

            try
            {
                // Obtener el producto desde la base de datos usando el código del producto
                var producto = _dbContext.Productos.Find(detalleDto.CodigoProducto);
                if (producto == null)
                {
                    return BadRequest(new { mensaje = "El producto no existe." });
                }

                // Calcular el total multiplicando el precio unitario del producto por la cantidad
                var total = producto.PrecioUnitario * detalleDto.Cantidad;

                // Crear la entidad DetallePedido a partir del DTO
                var detalle = new DetallePedido
                {
                    CodigoPedido = detalleDto.CodigoPedido,
                    CodigoProducto = detalleDto.CodigoProducto,
                    Cantidad = detalleDto.Cantidad,
                    Total = total // Asignar el total calculado
                };

                // Obtener el último número de línea para este pedido
                var ultimoNumeroLinea = _dbContext.DetallePedidos
                    .Where(dp => dp.CodigoPedido == detalle.CodigoPedido)
                    .OrderByDescending(dp => dp.NumeroLinea)
                    .Select(dp => dp.NumeroLinea)
                    .FirstOrDefault();

                detalle.NumeroLinea = ultimoNumeroLinea + 1; // Asignar el siguiente número de línea

                _dbContext.DetallePedidos.Add(detalle);

                _dbContext.SaveChanges(); // Guardar cambios en la base de datos
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Detalle de pedido creado", response = detalle });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }






        // Obtener detalles de un pedido específico
        [HttpGet]
        [Route("detalle/{codigoPedido}")]
        public IActionResult Detalle(int codigoPedido)
        {
            try
            {
                var detalles = _dbContext.DetallePedidos.Where(dp => dp.CodigoPedido == codigoPedido).ToList();
                if (detalles == null || !detalles.Any())
                {
                    return NotFound(new { mensaje = "No se encontraron detalles para el pedido especificado." });
                }
                return Ok(new { mensaje = "ok", response = detalles });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // Actualizar detalles de un pedido
        [HttpPatch]
        [Route("actualizar/{codigoPedido}/{numeroLinea}")]
        public IActionResult Actualizar(int codigoPedido, int numeroLinea, [FromBody] DetallePedido detalle)
        {
            if (detalle == null)
            {
                return BadRequest(new { mensaje = "El detalle no puede ser nulo." });
            }

            try
            {
                var detalleDB = _dbContext.DetallePedidos.Find(codigoPedido, numeroLinea);
                if (detalleDB == null)
                {
                    return NotFound(new { mensaje = "Detalle no encontrado." });
                }

                // Actualiza solo las propiedades que se desean modificar
                detalleDB.CodigoProducto = detalle.CodigoProducto;
                detalleDB.Cantidad = detalle.Cantidad;
                detalleDB.Total = detalle.Total;

                _dbContext.SaveChanges();
                return Ok(new { mensaje = "Detalle actualizado", response = detalleDB });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // Eliminar un detalle de pedido
        [HttpDelete]
        [Route("eliminar/{codigoPedido}/{numeroLinea}")]
        public IActionResult Eliminar(int codigoPedido, int numeroLinea)
        {
            try
            {
                var detalle = _dbContext.DetallePedidos.Find(codigoPedido, numeroLinea);
                if (detalle == null)
                {
                    return NotFound(new { mensaje = "Detalle no encontrado." });
                }

                _dbContext.DetallePedidos.Remove(detalle);
                _dbContext.SaveChanges();
                return Ok(new { mensaje = "Detalle eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

    }
}

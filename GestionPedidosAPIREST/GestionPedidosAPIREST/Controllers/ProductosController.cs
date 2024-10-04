using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using GestionPedidosAPIREST.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionPedidosAPIREST.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        public readonly GestionPedidoContext _dbcontext;

        public ProductosController(GestionPedidoContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult Lista() {
            List<Producto> productos = new List<Producto>();

            try
            {
                productos = _dbcontext.Productos.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = productos });
            }
            catch (Exception ex) { 
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = productos  });
            }
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear([FromBody] Producto producto)
        {
            try
            {
                _dbcontext.Productos.Add(producto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, new { mensaje = "ok", response = producto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("detalle/{codigoProducto}")]
        public IActionResult Detalle(int codigoProducto)
        {
            try
            {
                var producto = _dbcontext.Productos.Find(codigoProducto);
                if (producto == null)
                {
                    return NotFound(new { mensaje = "Producto no encontrado" });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = producto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPatch]
        [Route("actualizar/{codigoProducto}")]
        public IActionResult Actualizar(int codigoProducto, [FromBody] Producto producto)
        {
            try
            {
                var productoDB = _dbcontext.Productos.Find(codigoProducto);
                if (productoDB == null)
                {
                    return NotFound(new { mensaje = "Producto no encontrado" });
                }
                productoDB.DescripcionProducto = producto.DescripcionProducto;
                productoDB.PrecioUnitario = producto.PrecioUnitario;
                productoDB.Moneda = producto.Moneda;
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = productoDB });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("eliminar/{codigoProducto}")]
        public IActionResult Eliminar(int codigoProducto)
        {
            try
            {
                var producto = _dbcontext.Productos.Find(codigoProducto);
                if (producto == null)
                {
                    return NotFound(new { mensaje = "Producto no encontrado" });
                }
                _dbcontext.Productos.Remove(producto);
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

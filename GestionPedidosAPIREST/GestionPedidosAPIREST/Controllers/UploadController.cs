using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using EFCore.BulkExtensions;
using GestionPedidosAPIREST.Models;
using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;

namespace GestionPedidosAPIREST.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly GestionPedidoContext _context;

        public UploadController(GestionPedidoContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("uploadexcel")]
        [Consumes("multipart/form-data")]
        public IActionResult UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { mensaje = "Archivo no válido" });

            try
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    IWorkbook workbook = new XSSFWorkbook(stream);

                    // Procesar la hoja de clientes
                    ISheet sheetClientes = workbook.GetSheet("Clientes");
                    var clientes = ProcesarClientes(sheetClientes);

                    _context.BulkInsert(clientes);

                    // Procesar la hoja de productos
                    ISheet sheetProductos = workbook.GetSheet("Productos");
                    var productos = ProcesarProductos(sheetProductos);

                    _context.BulkInsert(productos);

                    // Procesar la hoja de cabecera pedidos
                    ISheet sheetCabeceraPedidos = workbook.GetSheet("CabeceraPedidos");
                    var cabeceraPedidos = ProcesarCabeceraPedidos(sheetCabeceraPedidos);

                    _context.BulkInsert(cabeceraPedidos);

                    // Procesar la hoja de detalle pedidos
                    ISheet sheetDetallePedidos = workbook.GetSheet("DetallePedidos");
                    var detallePedidos = ProcesarDetallePedidos(sheetDetallePedidos);
               
                    _context.BulkInsert(detallePedidos);

                    return Ok(new { mensaje = "Datos cargados exitosamente" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { mensaje = "Error al procesar el archivo" });
            }
        }

        private List<Cliente> ProcesarClientes(ISheet sheet)
        {
            var clientes = new List<Cliente>();

            for (int i = 1; i <= sheet.LastRowNum; i++) // Empieza desde la fila 1 (para saltar los encabezados)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;

                var cliente = new Cliente
                {
                    RucDni = Convert.ToInt64(row.GetCell(0).ToString()),
                    RazonSocial = row.GetCell(1).ToString(),
                    DireccionEntrega = row.GetCell(2).ToString(),
                    DireccionFiscal = row.GetCell(3).ToString(),
                    FechaRegistro = row.GetCell(4).DateCellValue,
                    UsuarioRegistro = row.GetCell(5).DateCellValue
                };
                clientes.Add(cliente);
            }

            return clientes;
        }

        private List<Producto> ProcesarProductos(ISheet sheet)
        {
            var productos = new List<Producto>();

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;

                var producto = new Producto
                {
                    DescripcionProducto = row.GetCell(0).ToString(),
                    PrecioUnitario = Convert.ToDecimal(row.GetCell(1).ToString()),
                    Moneda = row.GetCell(2).ToString()
                };
                productos.Add(producto);
            }

            return productos;
        }

        private List<CabeceraPedido> ProcesarCabeceraPedidos(ISheet sheet)
        {
            var cabeceraPedidos = new List<CabeceraPedido>();

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;

                var cabeceraPedido = new CabeceraPedido
                {
                    CodigoCliente = Convert.ToInt32(row.GetCell(0).ToString()),
                    DireccionEntrega = row.GetCell(1).ToString(),
                    Subtotal = Convert.ToDecimal(row.GetCell(2).ToString()),
                    CantidadTotal = Convert.ToInt64(row.GetCell(3).ToString()),
                    Igv = Convert.ToDecimal(row.GetCell(4).ToString()),
                    Total = Convert.ToDecimal(row.GetCell(5).ToString())
                };
                cabeceraPedidos.Add(cabeceraPedido);
            }

            return cabeceraPedidos;
        }

        private List<DetallePedido> ProcesarDetallePedidos(ISheet sheet)
        {
            var detallePedidos = new List<DetallePedido>();

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;

                var codigoPedido = Convert.ToInt32(row.GetCell(0).ToString());
                var numeroLinea = Convert.ToInt32(row.GetCell(1).ToString());
                var codigoProducto = Convert.ToInt32(row.GetCell(2).ToString());
                var cantidad = Convert.ToInt64(row.GetCell(3).ToString());

                // Obtener el producto desde la base de datos
                var producto = _context.Productos.Find(codigoProducto);
                if (producto == null)
                {
                    throw new Exception($"El producto con código {codigoProducto} no existe.");
                }

                // Calcular el total
                var total = producto.PrecioUnitario * cantidad;

                // Verificar si el detalle con el mismo código de pedido y número de línea ya existe
                var existingDetail = _context.DetallePedidos
                    .Any(dp => dp.CodigoPedido == codigoPedido && dp.NumeroLinea == numeroLinea);

                if (existingDetail)
                {
                    throw new Exception($"El detalle con código de pedido {codigoPedido} y número de línea {numeroLinea} ya existe.");
                }

                var detallePedido = new DetallePedido
                {
                    CodigoPedido = codigoPedido,
                    NumeroLinea = numeroLinea, // Ahora se asigna directamente desde el Excel
                    CodigoProducto = codigoProducto,
                    Cantidad = cantidad,
                    Total = total
                };
                detallePedidos.Add(detallePedido);
            }

            return detallePedidos;
        }



    }
}

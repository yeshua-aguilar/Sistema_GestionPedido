using Microsoft.AspNetCore.Mvc;

namespace GestionPedidoAPI.Controllers
{
    [ApiController]
    [Route("Productos")]
    public class ProductoController : ControllerBase
    {

        [HttpGet(Name = "listar")]
        {
            return messeng = "hola"
        }
    }
}

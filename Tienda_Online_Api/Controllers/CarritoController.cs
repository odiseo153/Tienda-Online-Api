using Controlador;
using Microsoft.AspNetCore.Mvc;

namespace Tienda_Online_Api.Controllers
{
    public class CarritoController : Controller
    {
        private CarritoControlador carrito;

        public CarritoController(CarritoControlador pedido)
        {
            this.carrito = pedido;
        }


        [HttpGet("carrito")]
        public IActionResult Get()
        {
            return carrito.Carrito();
        }
    }
}

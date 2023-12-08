using Controlador;
using Controlador.Interfazes;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTOS.CREATE_DTO;
using System.ComponentModel.DataAnnotations;

namespace Tienda_Online_Api.Controllers
{
    public class PedidoController : Controller
    {
        private PedidoControlador pedido;

        public PedidoController(PedidoControlador pedido)
        {
            this.pedido = pedido;
        }


        [HttpGet("PedidoDetallados")]
        public IActionResult PedirDetallado()
        {
            return pedido.DetallesPedidos();
        }

        [HttpGet("PedidoDetallado")]
        public IActionResult PedirDetallados([Required] string idPedido)
        {
            return pedido.DetallesPedido(idPedido);
        }


        [HttpPost("Realizar_Pedido")]
        public IActionResult Pedir([Required] CrearPedidoDTO entidad)
        {
            return pedido.Agregar(entidad);
        }

        [HttpPut("Actualizar_Pedido")]
        public IActionResult ActualizarPedido([Required]string Idpedido, [Required] string idUser, [Required] string estado)
        {
            return pedido.Actualizar(Idpedido,idUser,estado);
        }

        [HttpGet("Pedidos")]
        public IActionResult Pedido()
        {
            return pedido.ObtenerTodos();
        }

        [HttpGet("Pedido/{idPedido}")]
        public IActionResult PedidoPorId([Required] string idPedido)
        {
            return pedido.ObtenerPorId(idPedido);
        }

        [HttpDelete("Eliminar_Pedido/{idPedido}")]
        public IActionResult Eliminar([Required] string idPedido)
        {
            return pedido.Eliminar(idPedido);
        }

    }
}

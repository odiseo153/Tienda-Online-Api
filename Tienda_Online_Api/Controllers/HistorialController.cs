using Controlador;
using Controlador.Interfazes;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTOS.CREATE_DTO;

namespace Tienda_Online_Api.Controllers
{
    public class HistorialController : Controller
    {
        private HistorialControlador historial;

        public HistorialController(HistorialControlador historial)
        {
            this.historial = historial;
        }

        [HttpGet("Historial")]
        public IActionResult Index()
        {
            return historial.ObtenerTodos();
        }


        [HttpGet("Historial/{id}")]
        public IActionResult Index(string id)
        {
            return historial.ObtenerPorId(id);
        }
    }
}

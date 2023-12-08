using Controlador.Interfazes;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTOS.CREATE;
using Modelo.DTOS.CREATE_DTO;
using System.ComponentModel.DataAnnotations;

namespace Tienda_Online_Api.Controllers
{
    public class ProductoController : Controller
    {
        private IMetodosBasicos<CrearProductosDTO> producto;

        public ProductoController(IMetodosBasicos<CrearProductosDTO> product)
        {
            this.producto = product;
        }



        [HttpGet("Productos")]
        public IActionResult get()
        {
            return new JsonResult(producto.ObtenerTodos());
        }

        [HttpGet("Productos/{id}")]
        public IActionResult get([Required] string id)
        {
            return new JsonResult(producto.ObtenerPorId(id));
        }

        [HttpPost("Productos_add")]
        public IActionResult Agregar(CrearProductosDTO product)
        {
            return new JsonResult(producto.Agregar(product));
        }

        [HttpPut("Productos_update/{id}")]
        public IActionResult Actualizar([Required] string id, [Required] string idUser, [Required] CrearProductosDTO product)
        {
            return new JsonResult(producto.Actualizar(id,idUser,product));
        }


        [HttpDelete("Productos_delete/{id}")]
        public IActionResult Delete([Required] string id)
        {
            return new JsonResult(producto.Eliminar(id));
        }

    }
}

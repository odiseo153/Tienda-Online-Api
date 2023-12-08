using Controlador.Interfazes;
using Controlador.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTOS.CREATE;
using Modelo.DTOS.GET;
using System.ComponentModel.DataAnnotations;
using Tienda_Online_Api.Modelos;

namespace Tienda_Online_Api.Controllers
{
    public class UsuariosController : Controller
    {
        private IMetodosBasicos<CrearUsuariosDTO> user;

        public UsuariosController(IMetodosBasicos<CrearUsuariosDTO> usuario)
        {
            this.user = usuario;
        }

        [HttpGet("Usuarios")]
        public IActionResult get()
        {
            return new JsonResult(user.ObtenerTodos());
        }

        [HttpGet("Usuarios/{id}")]
        public IActionResult get([Required] string id)
        {
            return new JsonResult(user.ObtenerPorId(id));
        }


        [HttpPost("Usuarios_add")]
        public IActionResult Agregar([Required] CrearUsuariosDTO usuario)
        {
            return new JsonResult(user.Agregar(usuario));
        }

        [HttpPut("Usuarios_update/{id}")]
        public IActionResult Actualizar([Required] string id, [Required] CrearUsuariosDTO usuario)
        {
            return new JsonResult(user.Actualizar(id,"",usuario));
        }

        [HttpDelete("Usuarios_delete/{id}")]
        public IActionResult Delete([Required] string id)
        {
            return new JsonResult(user.Eliminar(id));
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Controlador.Interfazes
{
    public interface IMetodosBasicos<T>
    {
        JsonResult ObtenerPorId(string id);
        JsonResult ObtenerTodos();
        IActionResult Agregar(T entidad);
        IActionResult Actualizar(string id, string idUsuario, T entidad);
        IActionResult Eliminar(string id);
    }
}

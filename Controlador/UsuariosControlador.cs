using AutoMapper;
using Controlador.Interfazes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTOS.CREATE;
using Modelo.DTOS.GET;
using Tienda_Online_Api.Modelos;

namespace Controlador.Usuarios
{
    public class UsuariosControlador : IMetodosBasicos<CrearUsuariosDTO>
    {
        private TiendaOnlineContext context;
        private IMapper mapper;
        


        public UsuariosControlador(TiendaOnlineContext tiendaOnlineContext,IMapper mapper)
        {
            this.mapper = mapper;
            this.context = tiendaOnlineContext;
           
        }

        public IActionResult Actualizar(string id, string idUser, CrearUsuariosDTO entidad)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.Usuarioid.Equals(Guid.Parse(id)));

            if (usuario == null)
            {
                return new JsonResult(new
                {
                    message = $"No existe usuario con el ID {id}",
                    code = StatusCodes.Status404NotFound
                });
            }

            usuario.Nombre = entidad.Nombre ?? usuario.Nombre;
            usuario.Rol = entidad.Rol ?? usuario.Rol;
            usuario.Contraseña = entidad.Contraseña ?? usuario.Contraseña;
            usuario.Correoelectronico = entidad.Correoelectronico ?? usuario.Correoelectronico;

            context.SaveChanges();


            return new JsonResult(new
            {
                message = $"Usuario Actualizado Correctamente",
                Usuario_Nuevo = usuario,
                code = StatusCodes.Status200OK
            });
        }

        public IActionResult Agregar(CrearUsuariosDTO entidad)
        {
            if (entidad == null)
            {
                return new JsonResult(new
                {
                    message = $"No puede enviar Objeto nulo",
                    code = StatusCodes.Status400BadRequest
                });
            }
            var user = mapper.Map<Usuario>(entidad);
            user.Usuarioid = Guid.NewGuid();

            context.Usuarios.Add(user);
            context.SaveChanges();


            return new JsonResult(new
            {
                message = $"Usuario {entidad.Nombre} agregado con exito :)",
                code = StatusCodes.Status201Created
            });
        }

   

        public IActionResult Eliminar(string id)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.Usuarioid.Equals(Guid.Parse(id)));
            string nombre = usuario.Nombre;

            if (usuario == null)
            {
                return new JsonResult(new
                {
                    message = "no se encontro usuario con ese id",
                    code = StatusCodes.Status404NotFound
                });
            }
            context.Usuarios.Remove(usuario);
            context.SaveChanges();

            return new JsonResult(new
            {
                message = $"Usuario {nombre} eliminado con exito",
                code = StatusCodes.Status404NotFound
            });
        }

      
        public JsonResult ObtenerPorId(string id)
        {
            var usuario = mapper.Map<Usuario, GetUsuariosDTO>(context.Usuarios.FirstOrDefault(x => x.Usuarioid.Equals(Guid.Parse(id))));

            if(usuario == null)
            {
                return new JsonResult(new
                {
                    message = $"Usuario con el Id '{id}' no fue encontrado",
                    code = StatusCodes.Status404NotFound
                });
            }

            return new JsonResult(usuario);
        }

        public JsonResult ObtenerTodos()
        {
            var usuario = mapper.Map<List<Usuario>, List<GetUsuariosDTO>>(context.Usuarios.ToList());
            return new JsonResult(usuario);
        }
    }
}

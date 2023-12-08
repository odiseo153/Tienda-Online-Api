using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.DTOS.CREATE_DTO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tienda_Online_Api.Modelos;

namespace Controlador
{
    public class PedidoControlador 
    {
        private TiendaOnlineContext context;
        private IMapper mapper;

        public PedidoControlador(TiendaOnlineContext tiendaOnlineContext, IMapper mapper)
        {
            this.mapper = mapper;
            context = tiendaOnlineContext;
        }


        public IActionResult DetallesPedido(string idPedido)
        {
            var detalles = context.Detallespedidos
                .Include(x => x.Pedido)
                .ThenInclude(x => x.Usuario)
                .FirstOrDefault(x => x.Pedidoid.Equals(Guid.Parse(idPedido)));

            if (detalles == null)
            {
                return new JsonResult(new
                {
                    message = $"No existe Detalle de ese pedido",
                    code = StatusCodes.Status404NotFound
                });
            }

            // Crear un nuevo objeto con solo los detalles deseados
            var detallesReducidos = new
            {
                Detallepedidoid = detalles.Detallepedidoid,
                Pedidoid = detalles.Pedidoid,
                Productoid = detalles.Productoid,
                Cantidad = detalles.Cantidad,
                Preciounitario = detalles.Preciounitario,
                Pedido = new
                {
                    Pedidoid = detalles.Pedido.Pedidoid,
                    Usuarioid = detalles.Pedido.Usuarioid,
                    Fechapedido = detalles.Pedido.Fechapedido,
                    Estado = detalles.Pedido.Estado,
                    Usuario = new
                    {
                        Usuarioid = detalles.Pedido.Usuario.Usuarioid,
                        Nombre = detalles.Pedido.Usuario.Nombre,
                        Correoelectronico = detalles.Pedido.Usuario.Correoelectronico,
                        Rol = detalles.Pedido.Usuario.Rol
                        // Agregar otros campos del usuario según sea necesario
                    }
                    // Agregar otros campos del pedido según sea necesario
                }
                // Agregar otros campos del detalle según sea necesario
            };

            // Configuración de opciones de serialización para evitar ciclos de referencia
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // Otras opciones según tus necesidades
            };

            // Serializar el nuevo objeto a JSON
            var jsonString = JsonSerializer.Serialize(detallesReducidos, options);

            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                StatusCode = 200
            };
        }

        public IActionResult DetallesPedidos()
        {
            var detalles = context.Detallespedidos.ToList();

            return new JsonResult(detalles);
        }


        public IActionResult Actualizar(string idPedido,string idUser ,string estado)
        {
            var pedido = context.Pedidos.FirstOrDefault(x => x.Pedidoid.Equals(Guid.Parse(idPedido)));
            var usuario = context.Usuarios.FirstOrDefault(x => x.Usuarioid.Equals(Guid.Parse(idUser)));

            if (pedido == null)
            {
                return new JsonResult(new
                {
                    message = $"No existe pedido con el ID {idPedido}",
                    code = StatusCodes.Status404NotFound
                });
            }

            if (usuario == null)
            {
                return new JsonResult(new
                {
                    message = $"No existe usuario con el ID {idUser}",
                    code = StatusCodes.Status404NotFound
                });
            }

            if (!usuario.Usuarioid.Equals(pedido.Usuarioid))
            {
                return new JsonResult(new
                {
                    message = $"No existe relacion de este usuario con este pedido",
                    code = StatusCodes.Status404NotFound
                });
            }

            pedido.Estado = estado;
            
            Historialcambio historialcambio = new();
            historialcambio.Usuarioid = pedido.Usuarioid;
            historialcambio.Pedidoid = pedido.Pedidoid;
            historialcambio.Motivo = "Cambio de estado";
            historialcambio.Fechadecambio = DateTime.Now;

            context.Historialcambios.Add(historialcambio);
            context.SaveChanges();


            return new JsonResult(new
            {
                message = "Estado del pedido actualizado con exito",
                code = StatusCodes.Status200OK
            });
        }

        public IActionResult Agregar(CrearPedidoDTO entidad)
        {
            var usuario = context.Usuarios.FirstOrDefault(x => x.Usuarioid.Equals(entidad.Usuarioid));
            var producto = context.Productos.FirstOrDefault(x => x.Productoid.Equals(entidad.Productoid));


            if (producto == null)
            {
                return new JsonResult(new
                {
                    message = $"No existe producto con el ID {entidad.Productoid}",
                    code = StatusCodes.Status404NotFound
                });
            }


            if (usuario == null)
            {
                return new JsonResult(new
                {
                    message = $"No existe usuario con el ID {entidad.Usuarioid}",
                    code = StatusCodes.Status404NotFound
                });
            }

            Pedido pedido = new();
            pedido.Estado = "Comenzado";
            pedido.Usuarioid = usuario.Usuarioid;
            pedido.Fechapedido = DateTime.Now;

            context.Pedidos.Add(pedido);
            context.SaveChanges();

            Detallespedido detallespedido = new Detallespedido();
            detallespedido.Productoid= entidad.Productoid;
            detallespedido.Pedidoid = pedido.Pedidoid;
            detallespedido.Cantidad = entidad.Cantidad;
            detallespedido.Preciounitario = producto.Precio * entidad.Cantidad;


            context.Detallespedidos.Add(detallespedido);
            context.SaveChanges();

            Carritocompra carritocompra = new();
            carritocompra.Productoid = producto.Productoid;
            carritocompra.Usuarioid = usuario.Usuarioid;
            carritocompra.Cantidad= entidad.Cantidad;

            context.Carritocompras.Add(carritocompra);
            context.SaveChanges();

            return new JsonResult(new
            {
                message = "Pedido Agregado Correctamente",
                code = StatusCodes.Status201Created
            });

        }

        public IActionResult Eliminar(string id)
        {
            var pedido = context.Pedidos.FirstOrDefault(x => x.Pedidoid.Equals(Guid.Parse(id)));

            if(pedido == null)
            {
                return new JsonResult(new
                {
                    message = $"No existe pedido con el ID {id}",
                    code = StatusCodes.Status404NotFound
                });
            }

            context.Pedidos.Remove(pedido);
            context.SaveChanges();


            return new JsonResult(new
            {
                message = $"Pedido  eliminado",
                code =200
            });
        }

        public JsonResult ObtenerPorId(string id)
        {
            var pedido = context.Pedidos.FirstOrDefault(x => x.Pedidoid.Equals(Guid.Parse(id)));

            if( pedido == null)
            {
                return new JsonResult(new
                {
                    message = $"No se encontro pedido con ese ID",
                    code = StatusCodes.Status404NotFound
                });
            } 

            return new JsonResult(pedido);
        }

        public JsonResult ObtenerTodos()
        {
            return new JsonResult(context.Pedidos.ToList());
        }
    }
}

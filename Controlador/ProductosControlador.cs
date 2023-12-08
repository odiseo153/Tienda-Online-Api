using AutoMapper;
using Controlador.Interfazes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTOS.CREATE_DTO;
using Modelo.DTOS.GET;
using Modelo.DTOS.GET_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda_Online_Api.Modelos;

namespace Controlador
{
    public class ProductosControlador : IMetodosBasicos<CrearProductosDTO>
    {

        private TiendaOnlineContext context;
        private IMapper mapper;

        public ProductosControlador(TiendaOnlineContext tiendaOnlineContext, IMapper mapper)
        {
            this.mapper = mapper;
            context = tiendaOnlineContext;
        }

        public IActionResult Actualizar(string id, string idUsuario, CrearProductosDTO entidad)
        {
            var producto = context.Productos.FirstOrDefault(x => x.Productoid.Equals(Guid.Parse(id)));
            var usuarioCreador = context.Usuarios.FirstOrDefault(x => x.Usuarioid.Equals(Guid.Parse(idUsuario)));

            if (usuarioCreador == null)
            {
                return new JsonResult(new
                {
                    message = $"No existe usuario con el ID {idUsuario}",
                    code = StatusCodes.Status404NotFound
                });
            }


            if (producto == null)
            {
                return new JsonResult(new
                {
                    message = $"No existe producto con el ID {id}",
                    code = StatusCodes.Status404NotFound
                });
            }

            producto.Nombre = entidad.Nombre ?? producto.Nombre;
            producto.Precio = entidad.Precio == 0 ? producto.Precio : entidad.Precio;
            producto.Descripcion = entidad.Descripcion ?? producto.Descripcion;
            producto.Stock = entidad.Stock == 0 ? producto.Stock : entidad.Stock;

            Historialcambio historialcambio = new Historialcambio();
            historialcambio.Usuarioid = usuarioCreador.Usuarioid;
            historialcambio.Pedidoid = producto.Productoid;
            //historialcambio.Fechadecambio = DateTime.Now;
            historialcambio.Motivo = $"se cambio el producto {producto.Nombre}";


            context.Historialcambios.Add(historialcambio);
            context.SaveChanges();


            return new JsonResult(new
            {
                message = $"Producto Actualizado Correctamente",
                producto_Nuevo = producto,
                code = StatusCodes.Status200OK
            });

        }

        public IActionResult Agregar(CrearProductosDTO entidad)
        {

            if (entidad == null)
            {
                return new JsonResult(new
                {
                    message = $"No puede enviar Objeto nulo",
                    code = StatusCodes.Status400BadRequest
                });
            }
            var producto = mapper.Map<Producto>(entidad);
            producto.Productoid = Guid.NewGuid();

            context.Productos.Add(producto);
            context.SaveChanges();


            return new JsonResult(new
            {
                message = $"Producto {entidad.Nombre} agregado con exito :)",
                code = StatusCodes.Status201Created
            });

        }

        public IActionResult Eliminar(string id)
        {
            var producto = context.Productos.FirstOrDefault(x => x.Productoid.Equals(Guid.Parse(id)));
            string nombre = producto.Nombre;

            if (producto == null)
            {
                return new JsonResult(new
                {
                    message = "no se encontro producto con ese id",
                    code = StatusCodes.Status404NotFound
                });
            }
            context.Productos.Remove(producto);
            context.SaveChanges();

            return new JsonResult(new
            {
                message = $"Producto {nombre} eliminado con exito",
                code = StatusCodes.Status200OK
            });

        }

        public JsonResult ObtenerPorId(string id)
        {
            var producto = mapper.Map<Producto, CrearProductosDTO>(context.Productos.FirstOrDefault(x => x.Productoid.Equals(Guid.Parse(id))));

            if (producto == null)
            {
                return new JsonResult(new
                {
                    message = $"Producto con el Id '{id}' no fue encontrado",
                    code = StatusCodes.Status404NotFound
                });
            }

            return new JsonResult(producto);
        }

        public JsonResult ObtenerTodos()
        {
            var producto = mapper.Map<List<Producto>, List<CrearProductosDTO>>(context.Productos.ToList());
            return new JsonResult(producto);
        }
    }
}

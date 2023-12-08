using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Tienda_Online_Api.Modelos;

namespace Controlador
{
    public class CarritoControlador
    {
        private TiendaOnlineContext context;
        private IMapper mapper;

        public CarritoControlador(TiendaOnlineContext tiendaOnlineContext, IMapper mapper)
        {
            this.mapper = mapper;
            context = tiendaOnlineContext;
        }

        public IActionResult Carrito()
        {
            var carrito = context.Carritocompras
                .Include(x => x.Producto)
                .Include(x => x.Usuario)
                .ToList();

            // Crear una nueva estructura para representar el resultado deseado
            var resultado = new
            {
                Carrito = carrito.Select(item => new
                {
                    Carritoid = item.Carritoid,
                    Usuarioid = item.Usuarioid,
                    Productoid = item.Productoid,
                    Cantidad = item.Cantidad,
                    Producto = new
                    {
                        Productoid = item.Producto.Productoid,
                        Nombre = item.Producto.Nombre,
                        Descripcion = item.Producto.Descripcion,
                        Precio = item.Producto.Precio,
                        Stock = item.Producto.Stock
                        // Agrega más propiedades según sea necesario
                    },
                    Usuario = new
                    {
                        Usuarioid = item.Usuario.Usuarioid,
                        Nombre = item.Usuario.Nombre,
                        Correoelectronico = item.Usuario.Correoelectronico,
                        Contraseña = item.Usuario.Contraseña,
                        Rol = item.Usuario.Rol
                        // Agrega más propiedades según sea necesario
                    }
                }).ToList()
            };

            // Serializar el nuevo objeto a JSON
            var jsonString = JsonSerializer.Serialize(resultado, new JsonSerializerOptions
            {
                WriteIndented = true // Esto hace que la salida sea más legible con sangrías
                                     // Puedes agregar más opciones según sea necesario
            });

            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                StatusCode = 200
            };
        }

    }
}

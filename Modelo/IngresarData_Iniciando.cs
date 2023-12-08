using Microsoft.EntityFrameworkCore;
using Tienda_Online_Api.Modelos;

namespace Tienda_Online_Api
{
    public class IngresarData_Iniciando
    {
        private readonly TiendaOnlineContext _context;

        public IngresarData_Iniciando(TiendaOnlineContext context)
        {
            _context = context;
        }

        public IngresarData_Iniciando() { }

        public void Ingresar_Data_Para_No_Joder_Con_Datos()
        {
            // Obtén datos iniciales
            var usuarios = SeedUsuarios();
            var productos = SeedProductos();
            var pedidos = SeedPedidos(usuarios);
            var carritoCompra = SeedCarritoCompra(usuarios, productos);

            // Insertar usuarios
            foreach (var usuario in usuarios)
            {
                InsertarYGuardar(_context.Usuarios, usuario);
            }

            // Insertar productos
            foreach (var producto in productos)
            {
                InsertarYGuardar(_context.Productos, producto);
            }

            // Insertar pedidos
            foreach (var pedido in pedidos)
            {
                InsertarYGuardar(_context.Pedidos, pedido);
            }

            // Insertar elementos de carrito de compra
            foreach (var carrito in carritoCompra)
            {
                InsertarYGuardar(_context.Carritocompras, carrito);
            }
        }

        // Método genérico para insertar y guardar entidades en el contexto
        private void InsertarYGuardar<T>(DbSet<T> dbSet, T entidad) where T : class
        {
            dbSet.Add(entidad);
            _context.SaveChanges();
        }



        public static List<Usuario> SeedUsuarios()
        {
            var usuarios = new List<Usuario>();

            for (int i = 0; i < 10; i++)
            {
                var usuario = new Usuario
                {
                    // Inicializa las propiedades según tus necesidades
                    Usuarioid = Guid.NewGuid(),
                    Nombre = $"Usuario{i + 1}",
                    Correoelectronico = $"usuario{i + 1}@example.com",
                    Contraseña = "password123",
                    Rol = "cliente",
                };

                usuarios.Add(usuario);
            }
            

            return usuarios;
        }

        public static List<Carritocompra> SeedCarritoCompra(List<Usuario> usuarios, List<Producto> productos)
        {
            var carritos = new List<Carritocompra>();

            foreach (var usuario in usuarios)
            {
                foreach (var producto in productos)
                {
                    var carrito = new Carritocompra
                    {
                        // Inicializa las propiedades según tus necesidades
                        Carritoid = Guid.NewGuid(),
                        Cantidad = 2, // Cantidad ficticia
                        Productoid = producto.Productoid,
                        Usuarioid = usuario.Usuarioid,
                    };

                    carritos.Add(carrito);
                }
            }

            return carritos;
        }

        public static List<Detallespedido> SeedDetallesPedidos(List<Pedido> pedidos, List<Producto> productos)
        {
            var detallesPedidos = new List<Detallespedido>();

            foreach (var pedido in pedidos)
            {
                var detallePedido = new Detallespedido
                {
                    // Inicializa las propiedades según tus necesidades
                    Detallepedidoid = Guid.NewGuid(),
                    Cantidad = 2, // Cantidad ficticia
                    Pedidoid = pedido.Pedidoid,
                    Productoid = productos.First().Productoid, // Asociar a un producto (ajusta según tus datos)
                };

                detallesPedidos.Add(detallePedido);
            }

            return detallesPedidos;
        }


        public static List<Pedido> SeedPedidos(List<Usuario> usuarios)
        {
            var pedidos = new List<Pedido>();

            foreach (var usuario in usuarios)
            {
                var pedido = new Pedido
                {
                    // Inicializa las propiedades según tus necesidades
                    Pedidoid = Guid.NewGuid(),
                    Estado = "En proceso",
                    Fechapedido = DateTime.Now,
                    Usuarioid = usuario.Usuarioid, // Asocia el Pedido al Usuario
                };

                pedidos.Add(pedido);
            }

            return pedidos;
        }

        public static List<Producto> SeedProductos()
        {
            var productos = new List<Producto>();

            for (int i = 0; i < 10; i++)
            {
                var product = new Producto
                {
                    // Inicializa las propiedades según tus necesidades
                    Productoid = Guid.NewGuid(),
                    Nombre = "Producto "+i,
                    Descripcion = "Descripción del Producto "+i,
                    Precio = 29.99m,
                    Stock = 30,
                };

                productos.Add(product);
            }

            return productos;
        }

        // Luego, en el método Seed, llama a estos métodos:
      


    }
}

using System;
using System.Linq;
using ConsoleApp9;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TuProyecto
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configurar el servicio y contexto de la base de datos en memoria
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"))
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Agregar algunos datos de ejemplo a la base de datos en memoria
                dbContext.Categorias.Add(new Categoria { Nombre = "Electrónicos" });
                dbContext.Categorias.Add(new Categoria { Nombre = "Ropa" });

                dbContext.Productos.Add(new Producto { Nombre = "Laptop", Precio = 1000, CategoriaId = 1 });
                dbContext.Productos.Add(new Producto { Nombre = "Camiseta", Precio = 20, CategoriaId = 2 });

                dbContext.SaveChanges();

                // Crear una instancia de CarritoCompras y agregar productos
                var carrito = new CarritoCompras();
                var laptop = dbContext.Productos.FirstOrDefault(p => p.Nombre == "Laptop");
                var camiseta = dbContext.Productos.FirstOrDefault(p => p.Nombre == "Camiseta");

                carrito.AgregarProducto(laptop);
                carrito.AgregarProducto(camiseta);

                // Calcular el total del carrito
                var total = carrito.CalcularTotal();
                Console.WriteLine($"Total del carrito: ${total}");

                // Crear una factura
                var factura = new Factura
                {
                    Producto = laptop,
                    Cantidad = 1,
                    PrecioUnitario = laptop.Precio,
                    PrecioTotal = laptop.Precio
                };

                // Imprimir la factura
                Console.WriteLine($"Factura:\nProducto: {factura.Producto.Nombre}\nCantidad: {factura.Cantidad}\nPrecio Unitario: ${factura.PrecioUnitario}\nPrecio Total: ${factura.PrecioTotal}");

                // Crear y ejecutar una tarea en segundo plano para enviar una notificación
                var notificador = new Notificador();
                var tarea = notificador.SendNotificacionAsync();
                tarea.Wait();
            }

            // Ejemplo de cómo usar los controladores de la API (reemplaza con tu implementación)
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var productosController = new ProductosController(dbContext);

                // Uso de controlador de la API - Agrega un producto
                var producto = new Producto { Nombre = "Nuevo Producto", Precio = 50, CategoriaId = 1 };
                productosController.PostProducto(producto);

                var productos = productosController.GetProductos();
                foreach (var p in productos.Value)
                {
                    Console.WriteLine($"Producto: {p.Nombre}, Precio: {p.Precio}");
                }
            }

            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}

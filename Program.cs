using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp9
{
    internal class Program
    {
        /*static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            
        }*/
        public static async Task Main(string[] args)
        {
            var notificador = new Notificador();
            await Task.Delay(5000); // Espera 5 segundos
            notificador.SendNotificacion();
        }
    }

    

    // Configurar el contexto de la base de datos
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Producto>? Productos { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }
    }

}
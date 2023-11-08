using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    public class CarritoCompras
    {
        private List<Producto> productos = new List<Producto>();

        public void AgregarProducto (Producto producto)
        {
            productos.Add(producto);
        }

        public void EliminarProducto(Producto producto)
        {
            productos.Remove(producto);
        }

        public decimal CalcularTotal()
        {
            return productos.Sum(p => p.Precio);
        }
    }
}

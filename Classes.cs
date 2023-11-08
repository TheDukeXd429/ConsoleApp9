using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    public class Factura
    {
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
    }
    public class Producto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }

    public class Categoria
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }
}

using ConsoleApp9;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/productos")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Producto>> GetProductos()
    {
        return _context.Productos.ToList();
    }

    [HttpPost]
    public ActionResult<Producto> PostProducto(Producto producto)
    {
        _context.Productos.Add(producto);
        _context.SaveChanges();
        return CreatedAtAction("GetProductos", new { id = producto.Id }, producto);
    }

    [HttpGet("{id}")]
    public ActionResult<Producto> GetProducto(int id)
    {
        var producto = _context.Productos.Find(id);
        if (producto == null)
        {
            return NotFound();
        }
        return producto;
    }

    [HttpPut("{id}")]
    public IActionResult PutProducto(int id, Producto producto)
    {
        if (id != producto.Id)
        {
            return BadRequest();
        }
        _context.Entry(producto).State = EntityState.Modified;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProducto(int id)
    {
        var producto = _context.Productos.Find(id);
        if (producto == null)
        {
            return NotFound();
        }
        _context.Productos.Remove(producto);
        _context.SaveChanges();
        return NoContent();
    }
}

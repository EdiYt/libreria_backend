using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using libreria.Models;

namespace libreria.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibroController : ControllerBase
{
    private readonly DBLibreriaContext _context;

    public LibroController(DBLibreriaContext context)
    {
        _context = context;
    }

    // GET: api/Libro
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetLibros()
    {
        return await _context.Libros
            .Select(l => new  
            {
                l.IdLibro,
                l.Titulo,
                l.Precio,
                l.ImagenUrl,
                AutorNombre = _context.Autores.FirstOrDefault(a => a.IdAutor == l.IdAutor).Nombre,
                GeneroNombre = _context.Generos.FirstOrDefault(g => g.IdGenero == l.IdGenero).Nombre
            })
            .ToListAsync();
    }

    // POST: api/Libro
    [HttpPost]
    public async Task<IActionResult> PostLibro([FromBody] Libro libro)
    {
        try
        {
            // Verifica explícitamente los IDs
            var autorExiste = await _context.Autores.FindAsync(libro.IdAutor);
            var generoExiste = await _context.Generos.FindAsync(libro.IdGenero);

            if (autorExiste == null || generoExiste == null)
            {
                return BadRequest(new
                {
                    error = "IDs no existen",
                    autorExiste = autorExiste != null,
                    generoExiste = generoExiste != null
                });
            }

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, libro.IdLibro });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Error completo",
                message = ex.Message,
                inner = ex.InnerException?.Message
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutLibro(
    int id,
    [FromForm] Libro libro,
    IFormFile? imagen)
    {
        if (id != libro.IdLibro) return BadRequest();

        if (imagen != null && imagen.Length > 0)
        {
            var libroExistente = await _context.Libros.AsNoTracking().FirstOrDefaultAsync(l => l.IdLibro == id);
            if (!string.IsNullOrEmpty(libroExistente?.ImagenUrl))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", libroExistente.ImagenUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }
            libro.ImagenUrl = $"/images/{fileName}";
        }

        _context.Entry(libro).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Libro/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLibro(int id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro == null) return NotFound();
        _context.Libros.Remove(libro);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool LibroExists(int id)
    {
        return _context.Libros.Any(e => e.IdLibro == id);
    }
}
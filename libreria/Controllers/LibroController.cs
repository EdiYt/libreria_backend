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
    public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
    {
        return await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Genero)
            .ToListAsync();
    }

    // POST: api/Libro
    [HttpPost]
    public async Task<ActionResult<Libro>> PostLibro(Libro libro)
    {
        _context.Libros.Add(libro);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetLibro", new { id = libro.IdLibro }, libro);
    }

    // PUT: api/Libro/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLibro(int id, Libro libro)
    {
        if (id != libro.IdLibro) return BadRequest();
        _context.Entry(libro).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LibroExists(id)) return NotFound();
            else throw;
        }
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
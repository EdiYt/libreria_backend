using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using libreria.Models;

namespace libreria.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly DBLibreriaContext _context;

    public AutorController(DBLibreriaContext context)
    {
        _context = context;
    }

    // GET: api/Autor
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
    {
        return await _context.Autores.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> PostAutor([FromBody] Autor autor)
    {
        try
        {
            if (string.IsNullOrEmpty(autor.Nombre))
                return BadRequest("El nombre es requerido");

            autor.IdAutor = 0;

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutores), new { id = autor.IdAutor }, new
            {
                Id = autor.IdAutor,
                Nombre = autor.Nombre,
                Biografia = autor.Biografia
            });
        }
        catch (DbUpdateException dbEx)
        {
            return StatusCode(500, new
            {
                error = "Error en la base de datos",
                details = dbEx.InnerException?.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Error interno",
                details = ex.Message
            });
        }
    }

    // PUT: api/Autor/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAutor(int id, Autor autor)
    {
        if (id != autor.IdAutor) return BadRequest();
        _context.Entry(autor).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AutorExists(id)) return NotFound();
            else throw;
        }
        return NoContent();
    }

    // DELETE: api/Autor/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAutor(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        if (autor == null) return NotFound();
        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool AutorExists(int id)
    {
        return _context.Autores.Any(e => e.IdAutor == id);
    }
}
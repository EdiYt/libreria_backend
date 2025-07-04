using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using libreria.Models;

namespace libreria.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneroController : ControllerBase
{
    private readonly DBLibreriaContext _context;

    public GeneroController(DBLibreriaContext context)
    {
        _context = context;
    }

    // GET: api/Genero
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
    {
        return await _context.Generos.ToListAsync();
    }

    // POST: api/Genero
    [HttpPost]
    public async Task<ActionResult<Genero>> PostGenero(Genero genero)
    {
        _context.Generos.Add(genero);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetGenero", new { id = genero.IdGenero }, genero);
    }

    // PUT: api/Genero/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenero(int id, Genero genero)
    {
        if (id != genero.IdGenero) return BadRequest();
        _context.Entry(genero).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GeneroExists(id)) return NotFound();
            else throw;
        }
        return NoContent();
    }

    // DELETE: api/Genero/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenero(int id)
    {
        var genero = await _context.Generos.FindAsync(id);
        if (genero == null) return NotFound();
        _context.Generos.Remove(genero);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool GeneroExists(int id)
    {
        return _context.Generos.Any(e => e.IdGenero == id);
    }
}
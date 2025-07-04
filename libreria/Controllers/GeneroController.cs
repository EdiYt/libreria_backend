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
}
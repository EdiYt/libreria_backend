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

    // POST: api/Autor
    [HttpPost]
    public async Task<ActionResult<Autor>> PostAutor(Autor autor)
    {
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetAutor", new { id = autor.IdAutor }, autor);
    }
}
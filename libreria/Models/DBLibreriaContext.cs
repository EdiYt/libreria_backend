using Microsoft.EntityFrameworkCore;

namespace libreria.Models;

public class DBLibreriaContext : DbContext
{
    public DBLibreriaContext(DbContextOptions<DBLibreriaContext> options) : base(options) { }

    public DbSet<Autor> Autores { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Libro> Libros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(l => l.IdLibro);

            entity.HasOne<Autor>()
                  .WithMany()
                  .HasForeignKey(l => l.IdAutor)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Genero>()
                  .WithMany()
                  .HasForeignKey(l => l.IdGenero)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
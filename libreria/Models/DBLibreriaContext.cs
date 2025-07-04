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
        modelBuilder.Entity<Autor>().ToTable("Autores", t => t.ExcludeFromMigrations());
        modelBuilder.Entity<Genero>().ToTable("Generos", t => t.ExcludeFromMigrations());
        modelBuilder.Entity<Libro>().ToTable("Libros", t => t.ExcludeFromMigrations());

        modelBuilder.Entity<Libro>()
            .HasOne<Autor>()
            .WithMany()
            .HasForeignKey(l => l.IdAutor);
    }
}
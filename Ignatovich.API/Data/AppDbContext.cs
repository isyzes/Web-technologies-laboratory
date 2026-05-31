using Ignatovich.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ignatovich.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //BooksController;
        //AuthorController
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Author { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

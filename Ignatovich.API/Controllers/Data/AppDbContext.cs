using Ignatovich.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ignatovich.API.Controllers.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Author { get; set; }
}

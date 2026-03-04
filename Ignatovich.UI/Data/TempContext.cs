
using Ignatovich.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ignatovich.UI.Data
{
    public class TempContext : DbContext
    {
        public TempContext(DbContextOptions<TempContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Author { get; set; }
    }
}

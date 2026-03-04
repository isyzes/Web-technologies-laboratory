using Ignatovich.Domain.Entities;
using Ignatovich.Domain.Models;

namespace Ignatovich.UI.Services
{
    public class MemoryAuthorService : IAuthorService
    {
        public Task<ResponseData<List<Author>>> GetAuthorListAsync()
        {
            List<Author> authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "Александр", LastName = "Пушкин", NormalizedName="pushlin" },
                new Author { Id = 2, FirstName = "Лев", LastName = "Толстой", NormalizedName="tolstoy" },
                new Author { Id = 3, FirstName = "Фёдор", LastName = "Достоевский", NormalizedName="dostoevski" },
                new Author { Id = 4, FirstName = "Антон", LastName = "Чехов", NormalizedName="chehov" },
                new Author { Id = 5, FirstName = "Михаил", LastName = "Булгаков", NormalizedName="bulgakov" },
                new Author { Id = 6, FirstName = "Николай", LastName = "Гоголь", NormalizedName="gogol" }
            };
            var result = new ResponseData<List<Author>>()
            {
                Data = authors,
                Success = true
            };

            return Task.FromResult(result);

        }
    }
}

using Ignatovich.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ignatovich.API.Data;

public class DbInitializer
{
    /// <summary>
    /// Каноническое соответствие: Id автора → Id его книг.
    /// </summary>
    private static IReadOnlyDictionary<int, int[]> BookIdsByAuthorId => new Dictionary<int, int[]>
    {
        [1] = [1, 2, 3, 4, 5],
        [2] = [6, 7, 8, 9, 10],
        [3] = [11, 12, 13, 14, 15],
        [4] = [16, 17, 18, 19, 20],
        [5] = [21, 22, 23, 24],
        [6] = [25, 26, 27, 28],
    };

    public static async Task SeedData(WebApplication app)
    {
        var uri = "https://localhost:7281/";

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();

        if (!context.Author.Any() && !context.Books.Any())
        {
            var authors = new List<Author>
            {
                new() { Id = 1, FirstName = "Александр", LastName = "Пушкин", NormalizedName = "pushlin" },
                new() { Id = 2, FirstName = "Лев", LastName = "Толстой", NormalizedName = "tolstoy" },
                new() { Id = 3, FirstName = "Фёдор", LastName = "Достоевский", NormalizedName = "dostoevski" },
                new() { Id = 4, FirstName = "Антон", LastName = "Чехов", NormalizedName = "chehov" },
                new() { Id = 5, FirstName = "Михаил", LastName = "Булгаков", NormalizedName = "bulgakov" },
                new() { Id = 6, FirstName = "Николай", LastName = "Гоголь", NormalizedName = "gogol" },
            };

            await context.Author.AddRangeAsync(authors);
            await context.SaveChangesAsync();

            var books = new List<Book>
            {
                Book(1, 1, "Евгений Онегин", "images/evgeniy_onegin.jpg", 4.85m, uri),
                Book(2, 1, "Капитанская дочка", "images/kapitanskaya_dochka.jpg", 4.70m, uri),
                Book(3, 1, "Руслан и Людмила", "images/ruslan_i_lyudmila.jpg", 4.60m, uri),
                Book(4, 1, "Пиковая дама", "images/pikovaya_dama.jpg", 4.55m, uri),
                Book(5, 1, "Борис Годунов", "images/boris_godunov.jpg", 4.50m, uri),

                Book(6, 2, "Война и мир", "images/voyna_i_mir.jpg", 4.90m, uri),
                Book(7, 2, "Анна Каренина", "images/anna_karenina.jpg", 4.85m, uri),
                Book(8, 2, "Воскресение", "images/voskresenie.jpg", 4.40m, uri),
                Book(9, 2, "Крейцерова соната", "images/kreytserova_sonata.jpg", 4.30m, uri),
                Book(10, 2, "Смерть Ивана Ильича", "images/smert_ivana_ilicha.jpg", 4.45m, uri),

                Book(11, 3, "Преступление и наказание", "images/prestuplenie_i_nakazanie.jpg", 4.80m, uri),
                Book(12, 3, "Идиот", "images/idiot.jpg", 4.70m, uri),
                Book(13, 3, "Братья Карамазовы", "images/bratya_karamazovy.jpg", 4.85m, uri),
                Book(14, 3, "Бесы", "images/besy.jpg", 4.60m, uri),
                Book(15, 3, "Записки из подполья", "images/zapiski_iz_podpolya.jpg", 4.50m, uri),

                Book(16, 4, "Вишнёвый сад", "images/vishnevyy_sad.jpg", 4.70m, uri),
                Book(17, 4, "Чайка", "images/chayka.jpg", 4.60m, uri),
                Book(18, 4, "Три сестры", "images/tri_sestry.jpg", 4.65m, uri),
                Book(19, 4, "Дядя Ваня", "images/dyadya_vanya.jpg", 4.55m, uri),
                Book(20, 4, "Палата №6", "images/palata_6.jpg", 4.50m, uri),

                Book(21, 5, "Мастер и Маргарита", "images/master_i_margarita.jpg", 4.95m, uri),
                Book(22, 5, "Собачье сердце", "images/sobachye_serdtse.jpg", 4.75m, uri),
                Book(23, 5, "Белая гвардия", "images/belaya_gvardiya.jpg", 4.60m, uri),
                Book(24, 5, "Роковые яйца", "images/rokovye_yaytsa.jpg", 4.40m, uri),

                Book(25, 6, "Мёртвые души", "images/mertvye_dushi.jpg", 4.80m, uri),
                Book(26, 6, "Ревизор", "images/revizor.jpg", 4.70m, uri),
                Book(27, 6, "Вий", "images/viy.jpg", 4.40m, uri),
                Book(28, 6, "Тарас Бульба", "images/taras_bulba.jpg", 4.60m, uri),
            };

            await context.Books.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }

        //await FixAuthorBookRelationsAsync(context);
    }

    /// <summary>
    /// Исправляет внешние ключи AuthorId и подгружает навигационные свойства Author ↔ Books.
    /// </summary>
    public static async Task FixAuthorBookRelationsAsync(AppDbContext context)
    {
        var authors = await context.Author
            .Include(a => a.Books)
            .ToListAsync();

        var books = await context.Books.ToListAsync();
        if (books.Count == 0)
            return;

        var authorsById = authors.ToDictionary(a => a.Id);
        var changed = false;

        foreach (var (authorId, bookIds) in BookIdsByAuthorId)
        {
            if (!authorsById.TryGetValue(authorId, out var author))
                continue;

            author.Books ??= new List<Book>();

            foreach (var bookId in bookIds)
            {
                var book = books.FirstOrDefault(b => b.Id == bookId);
                if (book is null)
                    continue;

                if (book.AuthorId != authorId)
                {
                    book.AuthorId = authorId;
                    changed = true;
                }

                book.Author = author;

                if (!author.Books.Any(b => b.Id == book.Id))
                    author.Books.Add(book);
            }
        }

        if (changed)
            await context.SaveChangesAsync();
    }

    private static Book Book(int id, int authorId, string title, string imagePath, decimal rating, string uri) =>
        new()
        {
            Id = id,
            AuthorId = authorId,
            Title = title,
            Image = uri + imagePath,
            Rating = rating,
        };
}

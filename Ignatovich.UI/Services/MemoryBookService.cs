using Ignatovich.Domain.Entities;
using Ignatovich.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace Ignatovich.UI.Services
{
    public class MemoryBookService : IBookService
    {
        private readonly IConfiguration _config;
        List<Book> _books;
        List<Author> _authors;

        public MemoryBookService([FromServices] IConfiguration config, IAuthorService authorService)
        {
            _authors = authorService.GetAuthorListAsync().Result.Data;
            _config = config;
            SetupData();

        }

        public Task<ResponseData<Book>> CreateBookAsync(Book book, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Book>> GetBookByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<List<Book>>> GetBookListAsync(string? author)
        {
            ResponseData<List<Book>> result;

            int? authorId = null;

            if (author != null) 
            {
                authorId = _authors
                    .Find(a => a.NormalizedName.Equals(author))
                    ?.Id;
            }
            var data = _books
                .Where(b => authorId == null || b.AuthorId.Equals(authorId))?
                .ToList();
            if (data.Count == 0) 
            {
                result = ResponseData<List<Book>>.Error("Нет объектов у выбраного автора");

            } 
            else
            {
                result = ResponseData<List<Book>>.OK(data);
            }  

            return Task.FromResult(result);
        }

        public Task UpdateBookAsync(int id, Book book, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        private void SetupData()
        {
            _books = new List<Book>
            {
                // Александр Пушкин (AuthorId = 1) - 5 книг
                new Book { Id = 1, Title = "Евгений Онегин", 
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("pushlin")).Id, 
                    Author = _authors.Find(a => a.NormalizedName.Equals("pushlin")),
                    Image = "images/evgeniy_onegin.jpg", Rating = 4.85m },
                new Book { Id = 2, Title = "Капитанская дочка",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("pushlin")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("pushlin")),
                    Image = "images/kapitanskaya_dochka.jpg", Rating = 4.70m },
                new Book { Id = 3, Title = "Руслан и Людмила",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("pushlin")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("pushlin")),
                    Image = "images/ruslan_i_lyudmila.jpg", Rating = 4.60m },
                new Book { Id = 4, Title = "Пиковая дама",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("pushlin")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("pushlin")), 
                    Image = "images/pikovaya_dama.jpg", Rating = 4.55m },
                new Book { Id = 5, Title = "Борис Годунов",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("pushlin")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("pushlin")),
                    Image = "images/boris_godunov.jpg", Rating = 4.50m },
            
                // Лев Толстой (AuthorId = 2) - 5 книг
                new Book { Id = 6, Title = "Война и мир",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("tolstoy")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("tolstoy")),
                    Image = "images/voyna_i_mir.jpg", Rating = 4.90m },
                new Book { Id = 7, Title = "Анна Каренина",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("tolstoy")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("tolstoy")),
                    Image = "images/anna_karenina.jpg", Rating = 4.85m },
                new Book { Id = 8, Title = "Воскресение",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("tolstoy")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("tolstoy")),
                    Image = "images/voskresenie.jpg", Rating = 4.40m },
                new Book { Id = 9, Title = "Крейцерова соната",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("tolstoy")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("tolstoy")),
                    Image = "images/kreytserova_sonata.jpg", Rating = 4.30m },
                new Book { Id = 10, Title = "Смерть Ивана Ильича",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("tolstoy")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("tolstoy")),
                    Image = "images/smert_ivana_ilicha.jpg", Rating = 4.45m },
            
                // Фёдор Достоевский (AuthorId = 3) - 5 книг
                new Book { Id = 11, Title = "Преступление и наказание",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("dostoevski")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("dostoevski")),
                    Image = "images/prestuplenie_i_nakazanie.jpg", Rating = 4.80m },
                new Book { Id = 12, Title = "Идиот",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("dostoevski")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("dostoevski")),
                     Image = "images/idiot.jpg", Rating = 4.70m },
                new Book { Id = 13, Title = "Братья Карамазовы",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("dostoevski")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("dostoevski")),
                     Image = "images/bratya_karamazovy.jpg", Rating = 4.85m },
                new Book { Id = 14, Title = "Бесы",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("dostoevski")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("dostoevski")),
                     Image = "images/besy.jpg", Rating = 4.60m },
                new Book { Id = 15, Title = "Записки из подполья",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("dostoevski")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("dostoevski")),
                     Image = "images/zapiski_iz_podpolya.jpg", Rating = 4.50m },
            
                // Антон Чехов (AuthorId = 4) - 5 книг
                new Book { Id = 16, Title = "Вишнёвый сад",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("chehov")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("chehov")),
                      Image = "images/vishnevyy_sad.jpg", Rating = 4.70m },
                new Book { Id = 17, Title = "Чайка",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("chehov")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("chehov")),
                       Image = "images/chayka.jpg", Rating = 4.60m },
                new Book { Id = 18, Title = "Три сестры",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("chehov")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("chehov")),
                       Image = "images/tri_sestry.jpg", Rating = 4.65m },
                new Book { Id = 19, Title = "Дядя Ваня",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("chehov")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("chehov")),
                       Image = "images/dyadya_vanya.jpg", Rating = 4.55m },
                new Book { Id = 20, Title = "Палата №6",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("chehov")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("chehov")),
                       Image = "images/palata_6.jpg", Rating = 4.50m },
            
                // Михаил Булгаков (AuthorId = 5) - 4 книги
                new Book { Id = 21, Title = "Мастер и Маргарита",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("bulgakov")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("bulgakov")),
                       Image = "images/master_i_margarita.jpg", Rating = 4.95m },
                new Book { Id = 22, Title = "Собачье сердце",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("bulgakov")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("bulgakov")),
                        Image = "images/sobachye_serdtse.jpg", Rating = 4.75m },
                new Book { Id = 23, Title = "Белая гвардия",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("bulgakov")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("bulgakov")),
                        Image = "images/belaya_gvardiya.jpg", Rating = 4.60m },
                new Book { Id = 24, Title = "Роковые яйца",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("bulgakov")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("bulgakov")),
                        Image = "images/rokovye_yaytsa.jpg", Rating = 4.40m },
            
                // Николай Гоголь (AuthorId = 6) - 4 книги
                new Book { Id = 25, Title = "Мёртвые души",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("gogol")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("gogol")),
                        Image = "images/mertvye_dushi.jpg", Rating = 4.80m },
                new Book { Id = 26, Title = "Ревизор",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("gogol")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("gogol")),
                         Image = "images/revizor.jpg", Rating = 4.70m },
                new Book { Id = 27, Title = "Вий",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("gogol")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("gogol")),
                         Image = "images/viy.jpg", Rating = 4.40m },
                new Book { Id = 28, Title = "Тарас Бульба",
                    AuthorId = _authors.Find(a => a.NormalizedName.Equals("gogol")).Id,
                    Author = _authors.Find(a => a.NormalizedName.Equals("gogol")),
                         Image = "images/taras_bulba.jpg", Rating = 4.60m }
            };
        }
    }
}

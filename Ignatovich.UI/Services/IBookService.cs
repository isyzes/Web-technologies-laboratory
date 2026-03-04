using Ignatovich.Domain.Entities;
using Ignatovich.Domain.Models;

namespace Ignatovich.UI.Services
{
    public interface IBookService
    {
        public Task<ResponseData<List<Book>>> GetBookListAsync(string? author);
        public Task<ResponseData<Book>> GetBookByIdAsync(int id);
        public Task UpdateBookAsync(int id, Book book, IFormFile? formFile);
        public Task DeleteBookAsync(int id);
        public Task<ResponseData<Book>> CreateBookAsync(Book book, IFormFile? formFile);
    }
}

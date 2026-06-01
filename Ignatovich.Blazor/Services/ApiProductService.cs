using Ignatovich.Domain.Entities;

namespace Ignatovich.Blazor.Services;

public class ApiProductService(HttpClient Http) : IProductService<Book>
{
    List<Book> _books;
    int _currentPage = 1;
    int _totalPages = 1;
    public IEnumerable<Book> Products => _books;
    public int CurrentPage => _currentPage;
    public int TotalPages => _totalPages;
    public event Action ListChanged;

    public async Task GetProducts(int pageNo = 1)
    {
        // Отправить запрос http
        var result = await Http.GetAsync("");
        // В случае успешного ответа
        if (result.IsSuccessStatusCode)
        {
            // API возвращает массив Book[], а не ResponseData
            var books = await result.Content.ReadFromJsonAsync<List<Book>>() ?? [];
            // обновить параметры страниц
            _currentPage = pageNo;
            _totalPages = (int)Math.Ceiling(books.Count / (double)3);
            // получить нужную страницу
            _books = books.Skip((pageNo - 1) * 3).Take(3).ToList();
            ListChanged?.Invoke();
        }
        // В случае ошибки
        else
        {
            _books = null;
            _currentPage = 1;
            _totalPages = 0;
        }
    }
}

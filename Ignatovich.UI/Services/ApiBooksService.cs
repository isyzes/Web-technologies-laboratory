using System.Net;
using System.Net.Http.Json;
using Ignatovich.Domain.Entities;
using Ignatovich.Domain.Models;

namespace Ignatovich.UI.Services;

public class ApiBooksService : IBookService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthorService _authorService;

    public ApiBooksService(HttpClient httpClient, IAuthorService authorService)
    {
        _httpClient = httpClient;
        _authorService = authorService;
    }

    public async Task<ResponseData<Book>> CreateBookAsync(Book book, IFormFile? formFile)
    {
        if (formFile is { Length: > 0 })
        {
            var imageUrl = await UploadImageAsync(formFile);
            if (imageUrl is null)
            {
                return ResponseData<Book>.Error("Не удалось загрузить изображение");
            }

            book.Image = imageUrl;
        }

        var response = await _httpClient.PostAsJsonAsync("", book);

        if (!response.IsSuccessStatusCode)
        {
            return ResponseData<Book>.Error("Error write API");
        }

        var created = await response.Content.ReadFromJsonAsync<Book>();
        if (created is null)
        {
            return ResponseData<Book>.Error("Error write API");
        }

        await PopulateAuthorAsync(created);
        return ResponseData<Book>.OK(created);
    }

    public async Task DeleteBookAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<ResponseData<Book>> GetBookByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return ResponseData<Book>.Error("Книга не найдена");
        }

        if (!response.IsSuccessStatusCode)
        {
            return ResponseData<Book>.Error("Error write API");
        }

        var book = await response.Content.ReadFromJsonAsync<Book>();
        if (book is null)
        {
            return ResponseData<Book>.Error("Книга не найдена");
        }

        await PopulateAuthorAsync(book);
        return ResponseData<Book>.OK(book);
    }

    public async Task<ResponseData<List<Book>>> GetBookListAsync(string? author)
    {
        var response = await _httpClient.GetAsync("");

        if (!response.IsSuccessStatusCode)
        {
            return ResponseData<List<Book>>.Error("Error write API");
        }

        var books = await response.Content.ReadFromJsonAsync<List<Book>>() ?? [];
        var authors = await GetAuthorsAsync();

        if (authors is null)
        {
            return ResponseData<List<Book>>.Error("Error write API");
        }

        if (author != null)
        {
            var authorId = authors
                .Find(a => a.NormalizedName.Equals(author))
                ?.Id;

            books = books
                .Where(b => authorId != null && b.AuthorId == authorId)
                .ToList();

            if (books.Count == 0)
            {
                return ResponseData<List<Book>>.Error("Нет объектов у выбраного автора");
            }
        }

        PopulateAuthors(books, authors);
        return ResponseData<List<Book>>.OK(books);
    }

    public async Task UpdateBookAsync(int id, Book book, IFormFile? formFile)
    {
        if (formFile is { Length: > 0 })
        {
            var imageUrl = await UploadImageAsync(formFile);
            if (imageUrl is null)
            {
                throw new HttpRequestException("Не удалось загрузить изображение");
            }

            book.Image = imageUrl;
        }

        var response = await _httpClient.PutAsJsonAsync($"{id}", book);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Ошибка API ({(int)response.StatusCode}): {body}");
        }
    }

    private async Task<string?> UploadImageAsync(IFormFile? formFile)
    {
        if (formFile is null || formFile.Length == 0)
        {
            return null;
        }

        using var content = new MultipartFormDataContent();
        await using var ms = new MemoryStream();
        await formFile.CopyToAsync(ms);
        content.Add(new ByteArrayContent(ms.ToArray()), "file", formFile.FileName);

        var response = await _httpClient.PostAsync("upload", content);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var url = (await response.Content.ReadAsStringAsync()).Trim();
        return string.IsNullOrEmpty(url) ? null : url;
    }

    private async Task<List<Author>?> GetAuthorsAsync()
    {
        var authorsResult = await _authorService.GetAuthorListAsync();
        return authorsResult.Success ? authorsResult.Data : null;
    }

    private async Task PopulateAuthorAsync(Book book)
    {
        var authors = await GetAuthorsAsync();
        if (authors is not null)
        {
            book.Author = authors.Find(a => a.Id == book.AuthorId)!;
        }
    }

    private static void PopulateAuthors(IEnumerable<Book> books, List<Author> authors)
    {
        foreach (var book in books)
        {
            book.Author = authors.Find(a => a.Id == book.AuthorId)!;
        }
    }
}

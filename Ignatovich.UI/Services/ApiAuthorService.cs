using Ignatovich.Domain.Entities;
using Ignatovich.Domain.Models;

namespace Ignatovich.UI.Services;

public class ApiAuthorService : IAuthorService
{
    private readonly HttpClient _httpClient;

    public ApiAuthorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseData<List<Author>>> GetAuthorListAsync()
    {
        var response = await _httpClient.GetAsync("");

        if (response.IsSuccessStatusCode)
        {
            var authors = await response.Content.ReadFromJsonAsync<List<Author>>();
            return ResponseData<List<Author>>.OK(authors);
        }

        return ResponseData<List<Author>>.Error("Error write API");
    }
}

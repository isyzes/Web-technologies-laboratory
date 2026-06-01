using Ignatovich.Domain.Entities;

namespace Ignatovich.Blazor.Services;

public class ApiAuthorService(HttpClient http) : IAuthorService
{
    public async Task<Author?> GetAuthorAsync(int id)
    {
        var response = await http.GetAsync($"{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<Author>();
    }
}

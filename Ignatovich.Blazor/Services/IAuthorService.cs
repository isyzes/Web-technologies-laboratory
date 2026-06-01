using Ignatovich.Domain.Entities;

namespace Ignatovich.Blazor.Services;

public interface IAuthorService
{
    Task<Author?> GetAuthorAsync(int id);
}

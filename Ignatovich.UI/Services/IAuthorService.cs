using Ignatovich.Domain.Entities;
using Ignatovich.Domain.Models;

namespace Ignatovich.UI.Services
{
    public interface IAuthorService
    {
        public Task<ResponseData<List<Author>>> GetAuthorListAsync();
    }
}

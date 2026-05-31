using Ignatovich.Domain.Entities;
using Ignatovich.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ignatovich.UI.Areas.Admin.Pages;

[Authorize(Policy = "admin")]
public class IndexModel(IBookService bookService) : PageModel
{
    public IList<Book> Book { get; set; } = [];

    public async Task OnGetAsync()
    {
        var result = await bookService.GetBookListAsync(null);
        Book = result.Success && result.Data is not null ? result.Data : [];
    }
}

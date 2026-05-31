using Ignatovich.Domain.Entities;
using Ignatovich.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ignatovich.UI.Areas.Admin.Pages;

[Authorize(Policy = "admin")]
public class DetailsModel(IBookService bookService) : PageModel
{
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var result = await bookService.GetBookByIdAsync(id.Value);
        if (!result.Success || result.Data is null)
        {
            return NotFound();
        }

        Book = result.Data;
        return Page();
    }
}

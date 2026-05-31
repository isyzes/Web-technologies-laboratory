using Ignatovich.Domain.Entities;
using Ignatovich.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ignatovich.UI.Areas.Admin.Pages;

[Authorize(Policy = "admin")]
public class EditModel(IAuthorService authorService, IBookService bookService) : PageModel
{
    [BindProperty]
    public Book Book { get; set; } = default!;

    [BindProperty]
    public IFormFile? ImageFile { get; set; }

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
        await SetAuthorsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var imageFile = ImageFile ?? Request.Form.Files.GetFile(nameof(ImageFile));
        ModelState.Remove($"{nameof(Book)}.{nameof(Book.Author)}");

        if (!ModelState.IsValid)
        {
            await SetAuthorsAsync();
            return Page();
        }

        try
        {
            await bookService.UpdateBookAsync(Book.Id, Book, imageFile);
        }
        catch (HttpRequestException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await SetAuthorsAsync();
            return Page();
        }

        return RedirectToPage("./Index");
    }

    private async Task SetAuthorsAsync()
    {
        var authors = (await authorService.GetAuthorListAsync()).Data ?? [];
        ViewData["AuthorId"] = new SelectList(authors, "Id", "FirstName");
    }
}

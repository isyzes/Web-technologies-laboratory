using Ignatovich.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ignatovich.UI.Controllers
{
    public class ProductController(IAuthorService authorService, IBookService bookService) : Controller
    {
        
        
        public async Task<IActionResult> Index(string? author)
        {
            var authors = (await authorService.GetAuthorListAsync()).Data;
            var currentAuthor = (authors.Find(a => a.NormalizedName.Equals(author)));

            ViewData["Authors"] = authors;
            ViewData["currentAuthor"] = author != null ? currentAuthor.FirstName + " " + currentAuthor.LastName : "Все";
                   

            var result = await bookService.GetBookListAsync(author);

            if (!result.Success) {
                return NotFound();
            } 
            return View(result.Data);
        }
    }
}

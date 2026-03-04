using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ignatovich.Domain.Entities;
using Ignatovich.UI.Data;

namespace Ignatovich.UI.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Ignatovich.UI.Data.TempContext _context;

        public CreateModel(Ignatovich.UI.Data.TempContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FirstName");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

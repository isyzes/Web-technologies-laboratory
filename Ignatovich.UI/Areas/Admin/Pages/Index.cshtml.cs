using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ignatovich.Domain.Entities;
using Ignatovich.UI.Data;
using Ignatovich.UI.Services;
using Microsoft.AspNetCore.Authorization;

namespace Ignatovich.UI.Areas.Admin.Pages
{
    [Authorize(Policy = "Admin")]
    public class IndexModel(IBookService bookService) : PageModel
    {
        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Book = (await bookService.GetBookListAsync(null)).Data;
                
        }
    }
}

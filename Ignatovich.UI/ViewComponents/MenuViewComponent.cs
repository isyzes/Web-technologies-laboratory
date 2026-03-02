using Microsoft.AspNetCore.Mvc;

namespace Ignatovich.UI.ViewComponents;

public class MenuViewComponent : ViewComponent
{
    public IViewComponentResult Invoke() 
    {
        ViewData["Controller"] = Request.RouteValues["controller"]?.ToString()?.ToLower()?? string.Empty;
        ViewData["Area"] = Request.RouteValues["area"]?.ToString()?.ToLower()?? string.Empty;

        return View();
    }
}

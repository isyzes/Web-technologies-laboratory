using Ignatovich.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ignatovich.UI.Controllers;

public class ImageController(UserManager<AppUser> userMenader, IWebHostEnvironment env) : Controller
{
  
    public async Task<IActionResult> GetAvatar()
    {
        var user = await userMenader.GetUserAsync(User);
        if (user.Avatar != null)
        {
            return File(user.Avatar, "image/*");
        }
        var web = env.WebRootPath;
        var imgPath = Path.Combine(web, "images", "default-profile-picture.png");
        return File(imgPath, "image/*");
    }
}

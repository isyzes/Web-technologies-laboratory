using Ignatovich.UI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ignatovich.UI.Controllers;

[Authorize]
public class ImageController(UserManager<AppUser> userManager, IWebHostEnvironment env) : Controller
{
    public async Task<IActionResult> GetAvatar()
    {
        var user = await userManager.GetUserAsync(User);
        if (user?.Avatar is { Length: > 0 } avatar)
        {
            return File(avatar, GetContentType(avatar));
        }

        var imgPath = Path.Combine(env.WebRootPath, "images", "default-profile-picture.png");
        if (!System.IO.File.Exists(imgPath))
        {
            return NotFound();
        }

        return PhysicalFile(imgPath, "image/png");
    }

    private static string GetContentType(byte[] data)
    {
        if (data.Length >= 4 && data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
        {
            return "image/png";
        }

        if (data.Length >= 3 && data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF)
        {
            return "image/jpeg";
        }

        if (data.Length >= 6 && data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46)
        {
            return "image/gif";
        }

        return "image/jpeg";
    }
}

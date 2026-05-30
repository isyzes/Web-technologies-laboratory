using Microsoft.AspNetCore.Identity;

namespace Ignatovich.UI.Data;

public class AppUser : IdentityUser
{
    public byte[]? Avatar { get; set; }
}

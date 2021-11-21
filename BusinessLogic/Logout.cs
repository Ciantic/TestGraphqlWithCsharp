using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    public async Task<bool> Logout([Service] IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext;
        if (context is null)
        {
            throw new Exception("HTTP Context is missing");
        }
        await context.SignOutAsync(IdentityConstants.ApplicationScheme);
        return true;
    }
}

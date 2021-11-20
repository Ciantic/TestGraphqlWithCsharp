using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public class CurrentUser
{
    readonly UserManager<AppUser> _userManager;

    readonly ClaimsPrincipal _principal;

    public CurrentUser(ClaimsPrincipal principal, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _principal = principal;
        Console.WriteLine("Current user initiated");
    }

    public Guid Id
    {
        get { return Guid.Parse(_userManager.GetUserId(_principal)); }
    }

    public Task<AppUser> User
    {
        get { return GetUser(); }
    }

    private async Task<AppUser> GetUser()
    {
        var user = await _userManager.GetUserAsync(_principal);
        if (user is null)
        {
            throw new Exception("User may have been deleted, but it's claim is still valid?");
        }
        return user;
    }
}

using System.Security.Claims;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    /// <summary>
    /// Refresh the logged in user
    ///
    /// Should be called every thirty minutes, or when returning to the site
    /// after 30 minute pause
    /// </summary>
    /// <param name="user"></param>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<bool> Refresh(
        CurrentUser user,
        [Service] UserManager<AppUser> userManager,
        [Service] SignInManager<AppUser> signInManager,
        [Service] IServiceProvider services
    )
    {
        var appUser = await user.User;
        await UpdateLastActivity(appUser, userManager, services);
        await signInManager.RefreshSignInAsync(appUser);
        return true;
    }
}

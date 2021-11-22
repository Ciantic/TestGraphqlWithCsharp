using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public partial class BusinessLogic
{
    public class LoginInput
    {
        public string Email { get; set; } = "";
        public string PasswordPlain { get; set; } = "";
    }

    public async Task<AppUser> Login(
        LoginInput input,
        [Service] IHttpContextAccessor httpContextAccessor,
        [Service] UserManager<AppUser> userManager,
        [Service] SignInManager<AppUser> signInManager,
        [Service] Events events
    )
    {
        var context = httpContextAccessor.HttpContext;
        if (context is null)
            throw new Exception("HTTP Context is missing");

        var user = await userManager.FindByEmailAsync(input.Email);
        if (user is null)
            throw new Exception("Check your email or password");

        var check = await signInManager.CheckPasswordSignInAsync(
            user,
            input.PasswordPlain,
            user.LockoutEnabled
        );

        if (!check.Succeeded)
            throw new Exception("Unable to login: " + check.ToString());

        await signInManager.SignInAsync(user, true, IdentityConstants.ApplicationScheme);

        events.OnUserLoggedIn(user);

        return user;
    }
}

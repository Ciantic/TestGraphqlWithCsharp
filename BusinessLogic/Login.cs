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
        [Service] IOptions<IdentityOptions> identityOptions
    )
    {
        var context = httpContextAccessor.HttpContext;
        if (context is null)
        {
            throw new Exception("HTTP Context is missing");
        }

        var user = await userManager.FindByEmailAsync(input.Email);
        if (user is null)
        {
            throw new Exception("Check your email or password");
        }

        if (!await userManager.CheckPasswordAsync(user, input.PasswordPlain))
        {
            throw new Exception("Check your email or password");
        }

        var factory = new UserClaimsPrincipalFactory<AppUser>(userManager, identityOptions);
        var principal = await factory.CreateAsync(user);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(12),
            IsPersistent = true,
            IssuedUtc = DateTimeOffset.UtcNow,
        };
        await context.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            authProperties
        );
        return user;
    }
}

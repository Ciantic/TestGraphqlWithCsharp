using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    public class LoginInput
    {
        public string Email { get; set; } = "";
        public string PasswordPlain { get; set; } = "";
    }

    public async Task<IdentityUser> Login(
        LoginInput input,
        [Service] IHttpContextAccessor httpContextAccessor,
        [Service] UserManager<IdentityUser> userManager
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

        var claimsIdentity = new ClaimsIdentity(
            new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                // new Claim(ClaimTypes.Role, "Administrator"),
            },
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(12),
            IsPersistent = true,
            IssuedUtc = DateTimeOffset.UtcNow,
        };
        await context.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );
        return user;
    }
}

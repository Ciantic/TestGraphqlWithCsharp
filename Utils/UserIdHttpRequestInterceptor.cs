using System.Security.Claims;
using HotChocolate.Execution;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Identity;

public class CurrentUser
{
    readonly UserManager<AppUser> _userManager;

    readonly ClaimsPrincipal _principal;

    public CurrentUser(ClaimsPrincipal principal, UserManager<AppUser> requestExecutor)
    {
        _userManager = requestExecutor;
        _principal = principal;
    }

    public Guid Id
    {
        get { return Guid.Parse(_userManager.GetUserId(_principal)); }
    }

    public Task<AppUser> User
    {
        get { return _userManager.GetUserAsync(_principal); }
    }
}

public class UserIdHttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(
        HttpContext context,
        IRequestExecutor requestExecutor,
        IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken
    )
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
        var email = context.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
        if (userId != "" && email != "")
        {
            var userManager = context.RequestServices.GetRequiredService<UserManager<AppUser>>();
            requestBuilder.SetProperty("CurrentUser", new CurrentUser(context.User, userManager));
        }
        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}

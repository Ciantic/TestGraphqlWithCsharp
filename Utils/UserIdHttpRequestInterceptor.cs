using System.Security.Claims;
using HotChocolate.Execution;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Identity;

public class CurrentUser
{
    readonly UserManager<IdentityUser> _userManager;

    public CurrentUser(string userId, string email, UserManager<IdentityUser> requestExecutor)
    {
        this._userManager = requestExecutor;
        this.UserId = userId;
        this.Email = email;
    }

    public string UserId { get; }
    public string Email { get; }

    public Task<IdentityUser> User
    {
        get
        {

            return _userManager.FindByIdAsync(UserId);
        }
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
        string userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
        string email = context.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
        var m = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
        requestBuilder.SetProperty("CurrentUser", new CurrentUser(userId, email, m));
        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}

using System.Security.Claims;
using HotChocolate.Execution;
using HotChocolate.AspNetCore;

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

        requestBuilder.SetProperty("UserId", userId);
        // requestBuilder.SetProperty("IntegerValue", int.Parse(userId));
        // requestBuilder.SetProperty("ObjectValue", new User { Id = userId });

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}

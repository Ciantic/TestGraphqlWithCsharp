using HotChocolate.AspNetCore;

public class CustomHttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    /*
    public override ValueTask OnCreateAsync(
        HttpContext context,
        IRequestExecutor requestExecutor,
        IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken
    )
    {
        // AddCurrentUser(context, requestBuilder);
        // AddIdempotencyKey(context, requestBuilder);
        // requestBuilder.

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
    */
    /*
    private static void AddCurrentUser(HttpContext context, IQueryRequestBuilder requestBuilder)
    {
        if (context.User.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
        {
            var userManager = context.RequestServices.GetRequiredService<UserManager<AppUser>>();
            requestBuilder.SetProperty("CurrentUser", new CurrentUser(context.User, userManager));
        }
    }

    private static void AddIdempotencyKey(HttpContext context, IQueryRequestBuilder requestBuilder)
    {
        var id = context.Request.Headers["Idempotency-Key"].FirstOrDefault();
        if (id != null && id != "")
        {
            try
            {
                requestBuilder.SetProperty("IdempotencyKey", Guid.Parse(id));
            }
            catch (FormatException) { }
        }
    }
    */
}

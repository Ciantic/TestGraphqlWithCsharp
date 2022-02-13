using Microsoft.EntityFrameworkCore;

public readonly record struct OnRegisterEvent(
    AppUser User,
    DbContext Context,
    IServiceProvider Services
)
{
    private static event EventHandler<OnRegisterEvent>? Handler;

    public static void Trigger(AppUser user, DbContext context, IServiceProvider services)
    {
        Handler?.Invoke(null, new OnRegisterEvent(user, context, services));
    }

    public static void Listen(EventHandler<OnRegisterEvent> listener)
    {
        Handler += listener;
    }
}

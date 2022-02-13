public readonly record struct OnLoginEvent(AppUser User, IServiceProvider Services)
{
    private static event EventHandler<OnLoginEvent>? Handler;

    public static void Trigger(AppUser user, IServiceProvider services)
    {
        Handler?.Invoke(null, new OnLoginEvent(user, services));
    }

    public static void Listen(EventHandler<OnLoginEvent> listener)
    {
        Handler += listener;
    }
}

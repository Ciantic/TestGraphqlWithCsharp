public readonly record struct OnUpdateLastActivityEvent(
    AppUser User,
    TimeSpan Duration,
    IServiceProvider Services
)
{
    private static event EventHandler<OnUpdateLastActivityEvent>? Handler;

    public static void Trigger(AppUser user, TimeSpan duration, IServiceProvider services)
    {
        Handler?.Invoke(null, new OnUpdateLastActivityEvent(user, duration, services));
    }

    public static void Listen(EventHandler<OnUpdateLastActivityEvent> listener)
    {
        Handler += listener;
    }
}

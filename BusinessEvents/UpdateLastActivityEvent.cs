public partial class BusinessEvents
{
    public readonly record struct UpdateLastActivityEventArgs(TimeSpan Duration, AppUser User);

    public event EventHandler<UpdateLastActivityEventArgs>? UpdateLastActivityHandler;

    public void OnUpdateLastActivity(TimeSpan duration, AppUser user)
    {
        UpdateLastActivityHandler?.Invoke(null, new UpdateLastActivityEventArgs(duration, user));
    }
}

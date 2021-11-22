public partial class Events
{
    public readonly record struct UserLoggedInEventArgs(AppUser User);

    public event EventHandler<UserLoggedInEventArgs>? UserLoggedIn;

    public void OnUserLoggedIn(AppUser user)
    {
        UserLoggedIn?.Invoke(null, new UserLoggedInEventArgs(user));
    }
}

public partial class Events
{
    public readonly record struct LoginEventArgs(AppUser User);

    public event EventHandler<LoginEventArgs>? LoginHandler;

    public void OnLogin(AppUser user)
    {
        LoginHandler?.Invoke(null, new LoginEventArgs(user));
    }
}

public partial class BusinessEvents
{
    public readonly record struct RegisterEvent(AppUser User);

    public event EventHandler<RegisterEvent>? RegisterHandler;

    public void OnRegister(AppUser user)
    {
        RegisterHandler?.Invoke(null, new RegisterEvent(user));
    }
}

using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    public class RegisterUserInput
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public AppUser? Register(
        RegisterUserInput input,
        AppDbContext dbContext,
        [Service] IServiceProvider services
    )
    {
        // TODO: Register user
        OnRegisterEvent.Trigger(null as AppUser, dbContext, services);
        return null;
    }
}

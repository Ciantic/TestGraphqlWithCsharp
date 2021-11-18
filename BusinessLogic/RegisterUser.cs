using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    public class RegisterUserInput
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public IdentityUser? RegisterUser(RegisterUserInput input, AppDbContext dbContext)
    {
        // TODO: Register user
        return null;
    }
}

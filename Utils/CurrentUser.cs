using Microsoft.AspNetCore.Identity;

public class CurrentUser
{
    private readonly UserManager<AppUser> _userManager;

    public CurrentUser(IHttpContextAccessor accessor, UserManager<AppUser> userManager)
    {
        var principal = accessor.HttpContext?.User;
        if (principal is null)
        {
            Console.WriteLine("principal is null, no user auth");
            return;
        }
        Id = Guid.Parse(userManager.GetUserId(principal));
        _userManager = userManager;
        Console.WriteLine("created current user");
    }

    public Guid Id { get; set; }

    public Task<AppUser> User
    {
        get { return GetUser(); }
    }

    private async Task<AppUser> GetUser()
    {
        var user = await _userManager.FindByIdAsync(Id.ToString());
        if (user is null)
        {
            throw new Exception("User may have been deleted, but it's claim is still valid?");
        }
        return user;
    }
}

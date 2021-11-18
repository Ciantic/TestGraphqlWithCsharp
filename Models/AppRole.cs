using Microsoft.AspNetCore.Identity;

public class AppRole : IdentityRole<Guid>
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";
}

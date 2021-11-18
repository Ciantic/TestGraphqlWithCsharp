using Microsoft.AspNetCore.Identity;

public class Thingie
{
    public Guid Id { get; set; }
    public IdentityUser Owner { get; set; } = null!;
    public string Title { get; set; } = "";
}

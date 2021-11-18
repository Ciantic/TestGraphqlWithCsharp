using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";
}

public class AppUserType : ObjectType<AppUser>
{
    protected override void Configure(IObjectTypeDescriptor<AppUser> descriptor)
    {
        descriptor.Authorize();

        // descriptor.Field(f => f.PasswordHash).Authorize();
        descriptor.Ignore(f => f.PasswordHash);
        // TODO: Can we completly disallow somehow here?
    }
}

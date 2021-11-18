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
        // Require some authorization to even fetch
        descriptor.Authorize();

        // Hide PasswordHash from graphql querying
        descriptor.Ignore(f => f.PasswordHash);
        // TODO: Can we disallow fetching some one elses user details in here?
    }
}

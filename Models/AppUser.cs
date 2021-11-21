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
        // Allow querying only specific fields
        descriptor.BindFields(BindingBehavior.Explicit);
        descriptor.Field(f => f.Id);
        descriptor.Field(f => f.Email);
        descriptor.Field(f => f.FirstName);
        descriptor.Field(f => f.LastName);
        // Require some authorization to even fetch
        // descriptor.Authorize();
        // Above seems to collide with login!
        //
        // TODO: Can we make additional security guarantees right here, like forbidding queries to other user's?
    }
}

using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    [Authorize]
    public async Task<IdentityUser?> CurrentUser([CurrentUser] CurrentUser user)
    {
        return await user.User;
    }
}

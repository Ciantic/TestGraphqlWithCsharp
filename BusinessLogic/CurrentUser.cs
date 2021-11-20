using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    public async Task<AppUser?> CurrentUser([CurrentUser] CurrentUser user)
    {
        return await user.User;
    }
}

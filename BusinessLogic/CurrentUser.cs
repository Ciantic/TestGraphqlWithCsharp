using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    public async Task<IdentityUser?> CurrentUser(
        [CurrentUserId] string userId,
        [Service] UserManager<IdentityUser> _userManager
    )
    {
        return await _userManager.FindByIdAsync(userId);
    }
}

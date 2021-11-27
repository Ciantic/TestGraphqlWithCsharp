using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    // This is not part of public API
    private static async Task UpdateLastActivity(
        AppUser appUser,
        UserManager<AppUser> userManager,
        [Service] Events events
    )
    {
        var lastActivityTimespan = DateTime.UtcNow - appUser.LastActivity;

        // Update last activity if it's older than 30 minutes
        if (appUser.LastActivity == null || lastActivityTimespan > TimeSpan.FromMinutes(30))
        {
            appUser.LastActivity = DateTime.UtcNow;
            var result = await userManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                throw new Exception("Unable to update users activity");
            }

            if (lastActivityTimespan is not null)
            {
                events.OnUpdateLastActivity(lastActivityTimespan.Value, appUser);
            }
        }
    }
}

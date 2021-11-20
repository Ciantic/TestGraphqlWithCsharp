using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public partial class BusinessLogic
{
    public class CreateThingieInput
    {
        public string Title { get; set; } = "";
    }

    // Only authorized users can create thingies

    [Authorize]
    async public Task<Thingie?> CreateThingie(
        CreateThingieInput input,
        AppDbContext dbContext,
        CurrentUser user
    )
    {
        var t = new Thingie() { Title = input.Title, Owner = await user.User };
        dbContext.Add(t);
        await dbContext.SaveChangesAsync();
        return t;
    }
}

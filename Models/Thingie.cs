using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class Thingie
{
    public Guid Id { get; set; }
    public IdentityUser Owner { get; set; } = null!;
    public string Title { get; set; } = "";
}

public partial class Query
{
    // Only authorized users can query their *own* thingies
    [Authorize]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Thingie> GetMyThingies(AppDbContext dbContext, [CurrentUser] CurrentUser user)
    {
        return dbContext.Thingies.Where(f => f.Owner.Id == user.UserId);
    }

    // However, if you are not logged in you can query everyones thingies (it's
    // counterintuitive but this is example)
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Thingie> GetAllThingies(AppDbContext dbContext)
    {
        return dbContext.Thingies;
    }
}

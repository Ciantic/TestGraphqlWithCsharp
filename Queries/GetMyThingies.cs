using HotChocolate.Authorization;

public partial class Query
{
    // Only authorized users can query their *own* thingies
    [Authorize]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Thingie> GetMyThingies(AppDbContext dbContext, CurrentUser user)
    {
        return dbContext.Thingies.Where(f => f.Owner.Id == user.Id);
    }
}

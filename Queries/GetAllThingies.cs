public partial class Query
{
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

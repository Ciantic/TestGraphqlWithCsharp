using Microsoft.AspNetCore.Identity;

public class Thingie
{
    public Guid Id { get; set; }
    public IdentityUser Owner { get; set; } = null!;
    public string Title { get; set; } = "";
}

public partial class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Thingie> Thingies(AppDbContext dbContext, [CurrentUserId] string userId)
    {
        return dbContext.Thingies.Where(f => f.Owner.Id == userId);
    }
}


/*

{
  persons(first: 1, order: {
  }, where: {
    firstName: {
      startsWith: "John"
    }
  }) {
    pageInfo {
      hasNextPage,
      endCursor
    }
    nodes {
      id
      firstName
    }
  }
}
*/

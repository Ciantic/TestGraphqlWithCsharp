using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Thingie> Thingies => Set<Thingie>();
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

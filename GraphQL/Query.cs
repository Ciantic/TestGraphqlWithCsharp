using Microsoft.AspNetCore.Identity;

public partial class Query { }

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

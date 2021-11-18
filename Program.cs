using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

public class ErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error?.Exception?.Message);
        Console.WriteLine("---------------------");
        Console.WriteLine(error?.Exception?.StackTrace);
        Console.ForegroundColor = ConsoleColor.White;
        return error.WithMessage("foo");
    }
}

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var graphQl = builder.Services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddHttpRequestInterceptor<UserIdHttpRequestInterceptor>()
            .AddMutationType<BusinessLogic>()
            .AddQueryType<Query>()
            .AddType<AppUserType>()
            .ConfigureResolverCompiler(
                c =>
                {
                    c.AddService<AppDbContext>();
                }
            )
            .AddFiltering()
            .AddSorting()
            .AddProjections();
        ;

        builder.Services
            .AddScoped<IInitDb, InitDbDevelopment>()
            .AddHttpContextAccessor()
            .AddAuthorization()
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>();

        builder.Services.Configure<IdentityOptions>(
            options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            }
        );

        builder.Services.ConfigureApplicationCookie(
            options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            }
        );
        if (builder.Environment.IsDevelopment())
        {
            graphQl
                .AddErrorFilter<ErrorFilter>()
                .ModifyRequestOptions(
                    d =>
                    {
                        d.IncludeExceptionDetails = true;
                    }
                );

            builder.Services.AddDbContext<AppDbContext>(
                (f) =>
                {
                    f.UseSqlite("Data Source=main.sqlite");
                    f.EnableSensitiveDataLogging(true);
                }
            );
        }
        else
        {
            throw new Exception("TODO");
        }

        var app = builder.Build();

        app.UseHttpsRedirection();
        // app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapGraphQL();

        using (var scoped = app.Services.CreateScope())
        {
            var init = scoped.ServiceProvider.GetRequiredService<IInitDb>();
            await init.Init();
        }

        app.Run();
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

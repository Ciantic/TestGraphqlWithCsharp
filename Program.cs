using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

public class ErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error?.Exception?.GetType().Name);
        Console.WriteLine(error?.Exception?.Message);
        Console.WriteLine("---------------------");
        Console.WriteLine(error?.Exception?.StackTrace);
        Console.ForegroundColor = ConsoleColor.White;
        return error!; //.WithMessage("foo");
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
            .AddMutationType<BusinessLogic>()
            .AddQueryType<Query>()
            .AddType<AppUserType>()
            .ConfigureResolverCompiler(
                c =>
                {
                    c.AddService<AppDbContext>();
                    c.AddService<CurrentUser>();
                    c.AddService<IdempotencyKey>();
                    // c.AddParameter<AppUser>(f => f.GetGlobalValue<AppUser>("CurrentUser")!);
                    // c.AddParameter<bool>(f => f.)
                }
            )
            .AddFiltering()
            .AddSorting()
            .AddProjections();

        builder.Services
            .AddScoped<IInitDb, InitDbDevelopment>()
            .AddScoped<CurrentUser>()
            .AddScoped<IdempotencyKey>()
            .AddHttpContextAccessor()
            .AddAuthorization()
            .AddAuthentication();

        builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

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
            OnLoginEvent.Listen(
                (e, f) =>
                {
                    Console.WriteLine("User logged in " + f.User.Email);
                }
            );
        }

        app.Run();
    }
}

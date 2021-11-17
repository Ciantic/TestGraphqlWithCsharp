using Microsoft.AspNetCore.Identity;

public class InitDbDevelopment : IInitDb
{
    private readonly AppDbContext appDbContext;
    private readonly IPasswordHasher<IdentityUser> passwordHasher;

    public InitDbDevelopment(
        AppDbContext appDbContext,
        IPasswordHasher<IdentityUser> passwordHasher
    )
    {
        this.appDbContext = appDbContext;
        this.passwordHasher = passwordHasher;
    }

    private async Task CreateTestDatabase()
    {
        await appDbContext.Database.EnsureDeletedAsync();
        await appDbContext.Database.EnsureCreatedAsync();
    }

    private static readonly Dictionary<String, int> usedEmails = new();

    private static string GenerateUniqueEmail(string prefix, string domain = "example.com")
    {
        var email = prefix + "@" + domain;
        if (usedEmails.ContainsKey(email))
        {
            var num = ++usedEmails[email];
            return prefix + num + "@" + domain;
        }
        else
        {
            usedEmails[email] = 1;
        }
        return email;
    }

    private int guidId = 1;
    private IdentityUser GenerateApplicationUser(string emailPrefix)
    {
        var email = GenerateUniqueEmail(emailPrefix);
        var id = Guid.Parse("00000000-0000-0000-0000-00000000000" + guidId++);
        return new IdentityUser()
        {
            Id = id.ToString(),
            Email = email,
            UserName = email,
            PasswordHash = passwordHasher.HashPassword(null!, "!Pass1"),
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = email.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true,
        };
    }

    private int guidIdThingie = 1;
    private Thingie GenerateThing(IdentityUser user)
    {
        var id = Guid.Parse("00000000-0000-0000-0001-00000000000" + guidIdThingie++);

        return new Thingie() { Id = id, Owner = user, Title = "foofoo" };
    }

    private async Task CreateTestDataByGeneration()
    {
        var entities = new List<object>();

        // Create businesses
        for (int i = 0; i < 3; i++)
        {
            var user = GenerateApplicationUser("test");
            var thingie = GenerateThing(user);
            entities.Add(user);
            entities.Add(thingie);
        }

        appDbContext.AddRange(entities);
        await appDbContext.SaveChangesAsync();
    }

    public async Task Init()
    {
        await CreateTestDatabase();
        await CreateTestDataByGeneration();
    }
}

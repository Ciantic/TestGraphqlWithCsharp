public class Thingie
{
    public Guid Id { get; set; }
    public AppUser Owner { get; set; } = null!;
    public string Title { get; set; } = "";
}

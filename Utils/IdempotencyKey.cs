using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public class IdempotencyKey
{
    public IdempotencyKey(IHttpContextAccessor accessor)
    {
        var id = accessor.HttpContext?.Request.Headers["Idempotency-Key"].FirstOrDefault();
        try
        {
            Value = Guid.Parse(id!);
        }
        catch (FormatException)
        {
            throw;
        }
        catch (ArgumentException)
        {
            throw;
        }
    }

    public Guid Value { get; set; }
}

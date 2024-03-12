using System.Security.Claims;

namespace MwTech.Infrastructure.Persistence.Extensions;

// Metoda rozszerzająca klasę ClaimsPrincipal
public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal model)
    {
        return model.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}

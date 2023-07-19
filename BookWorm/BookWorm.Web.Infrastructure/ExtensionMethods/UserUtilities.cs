namespace BookWorm.Web.Infrastructure.ExtensionMethods
{
    using System.Security.Claims;

    public static class UserUtilities
    {
        public static string? GetUserId(this ClaimsPrincipal claims)
        {
            return claims.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null; 
        }
    }
}

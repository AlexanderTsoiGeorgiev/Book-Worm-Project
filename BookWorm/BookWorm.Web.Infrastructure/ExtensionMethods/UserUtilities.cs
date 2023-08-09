namespace BookWorm.Web.Infrastructure.ExtensionMethods
{
    using System.Security.Claims;

    using static BookWorm.Common.ClaimNamesConstants;

    public static class UserUtilities
    {
        public static string? GetUserId(this ClaimsPrincipal claims)
        {
            return claims.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null; 
        }

        public static string GetUserFriendlyName(this ClaimsPrincipal claims)
        {
            return claims.FindFirst(FriendlyNameClaimName)?.Value ?? "Anonymous";
        }
    }
}

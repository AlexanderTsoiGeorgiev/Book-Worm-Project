namespace BookWorm.Services
{
    using System.Security.Claims;

    using BookWorm.Services.Interfaces;
    using Microsoft.AspNetCore.Http;

    public class HttpContextClaimsProvider : IClaimsProvider
    {
        public HttpContextClaimsProvider(IHttpContextAccessor httpContext)
        {
            ClaimsPrinciple = httpContext.HttpContext?.User!;
        }
        public ClaimsPrincipal ClaimsPrinciple { get; private set; }

    }
}

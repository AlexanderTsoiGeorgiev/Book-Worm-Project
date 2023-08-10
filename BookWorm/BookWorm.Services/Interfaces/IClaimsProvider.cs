namespace BookWorm.Services.Interfaces
{
    using System.Security.Claims;

    public interface IClaimsProvider
    {
        public ClaimsPrincipal ClaimsPrinciple { get; }
    }
}

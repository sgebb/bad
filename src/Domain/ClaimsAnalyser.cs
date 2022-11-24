using System.Security.Claims;

namespace Bad.Domain
{
    public partial class BadDomain
    {
        public static class ClaimsAnalyser
        {
            public static bool HasNightPrivileges(ClaimsPrincipal user)
            {
                return user.HasClaim(c => c.Issuer == "issuer" && c.Type == "adgroup" && c.Value == "guid");
            }
        }
    }
}

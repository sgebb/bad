using System.Security.Claims;

namespace Bad.Domain
{
    public partial class BadDomain
    {
        public static class ClaimsAnalyser
        {
            // lets assume that this code is valid
            public static bool HasNightPrivileges(ClaimsPrincipal user)
            {
                if (!user.Identities.FirstOrDefault().IsAuthenticated)
                {
                    return false;
                }

                if (!user.IsInRole("nightrole"))
                {
                    return false;
                }

                return user.HasClaim(c => c.Issuer == "issuer" && c.Type == "adgroup" && c.Value == "night-adgroup");
            }
        }
    }
}

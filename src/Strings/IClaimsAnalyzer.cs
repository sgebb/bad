using System.Security.Claims;

namespace Bad.Strings
{
    public interface IClaimsAnalyzer
    {
        bool HasNightPrivileges(ClaimsPrincipal user);
    }
}
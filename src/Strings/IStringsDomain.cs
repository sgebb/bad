using Bad.Database;
using System.Security.Claims;

namespace Bad.Strings
{
    public interface IStringsDomain
    {
        StringEntity? AddString(string value, ClaimsPrincipal user);
        IAsyncEnumerable<StringEntity> GetAllStrings();
        StringEntity? GetString(int id);
    }
}
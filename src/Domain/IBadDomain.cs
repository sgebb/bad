using Bad.Database;
using System.Security.Claims;

namespace Bad.Domain
{
    public interface IBadDomain
    {
        Task<StringDto?> AddString(string value, ClaimsPrincipal user);
        IAsyncEnumerable<StringDto> GetAllStrings();
        Task<StringDto?> GetString(int id);
    }
}
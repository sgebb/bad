using Bad.Database;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bad.Strings;

public class StringsDomain
{
    private readonly StringsDataAccess _dataAccess;

    public StringsDomain(StringsDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public IAsyncEnumerable<StringEntity> GetAllStrings()
    {
        return _dataAccess.GetAllStrings();
    }

    public StringEntity? GetString(int id)
    {
        return _dataAccess.GetString(id);
    }

    public StringEntity? AddString(string value, ClaimsPrincipal user)
    {
        var now = DateTimeOffset.UtcNow;
        var isNighttime = now.Hour > 0 && now.Hour < 7;
        if (isNighttime && !ClaimsAnalyser.HasNightPrivileges(user))
        {
            return null;
        }

        return _dataAccess.AddString(value);
    }
}

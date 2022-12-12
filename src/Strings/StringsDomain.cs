using Bad.Database;
using System.Security.Claims;

namespace Bad.Strings;

public class StringsDomain : IStringsDomain
{
    private readonly IStringsDataAccess _dataAccess;
    private readonly IClaimsAnalyzer _claimsAnalyzer;
    private readonly ITimeProvider _timeProvider;

    public StringsDomain(IStringsDataAccess dataAccess, ITimeProvider timeProvider, IClaimsAnalyzer claimsAnalyzer)
    {
        _dataAccess = dataAccess;
        _timeProvider = timeProvider;
        _claimsAnalyzer = claimsAnalyzer;
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
        var now = _timeProvider.Now();
        var isNighttime = now.Hour > 0 && now.Hour < 7;
        if (isNighttime && !_claimsAnalyzer.HasNightPrivileges(user))
        {
            return null;
        }

        return _dataAccess.AddString(value);
    }
}

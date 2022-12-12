using Bad.Database;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bad.Strings;

public class StringsDomain
{
    private readonly BadDbContext _context;

    public StringsDomain(BadDbContext context)
    {
        _context = context;
    }

    public IAsyncEnumerable<StringEntity> GetAllStrings()
    {
        return _context
            .Strings
            .AsAsyncEnumerable();
    }

    public StringEntity? GetString(int id)
    {
        var dbString = _context
            .Strings
            .AsNoTracking()
            .FirstOrDefault(s => s.Id == id);

        if (dbString == null)
        {
            return null;
        }

        return dbString;
    }

    public StringEntity? AddString(string value, ClaimsPrincipal user)
    {
        var now = DateTimeOffset.UtcNow;
        var isNighttime = now.Hour > 0 && now.Hour < 7;
        if (isNighttime && !ClaimsAnalyser.HasNightPrivileges(user))
        {
            return null;
        }

        var newString = new StringEntity(value);
        _context.Strings.Add(newString);
        _context.SaveChanges();
        return newString;
    }
}

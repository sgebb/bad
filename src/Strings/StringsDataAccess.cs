using Bad.Database;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bad.Strings;

public class StringsDataAccess : IStringsDataAccess
{
    private readonly BadDbContext _context;

    public StringsDataAccess(BadDbContext context)
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
        return _context
                .Strings
                .AsNoTracking()
                .FirstOrDefault(s => s.Id == id);
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

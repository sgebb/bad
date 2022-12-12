using Bad.Database;
using Microsoft.EntityFrameworkCore;

namespace Bad.Strings;

public class StringsDataAccess
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

    public StringEntity AddString(string value)
    {
        var newString = new StringEntity(value);
        _context.Strings.Add(newString);
        _context.SaveChanges();
        return newString;
    }
}

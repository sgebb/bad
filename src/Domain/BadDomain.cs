using Bad.Database;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bad.Domain
{
    public partial class BadDomain : IBadDomain
    {
        private readonly BadDbContext _context;

        public BadDomain(BadDbContext context)
        {
            _context = context;
        }

        public IAsyncEnumerable<StringDto> GetAllStrings()
        {
            return _context
                .Strings
                .Select(s => new StringDto(s.Id!.Value, s.Value))
                .AsAsyncEnumerable();
        }

        public async Task<StringDto?> GetString(int id)
        {
            var dbString = await _context
                .Strings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (dbString == null)
            {
                return null;
            }

            return new StringDto(dbString.Id!.Value, dbString.Value);
        }

        public async Task<StringDto?> AddString(string value, ClaimsPrincipal user)
        {
            var now = DateTimeOffset.UtcNow;
            var isNighttime = now.Hour > 0 && now.Hour < 7;
            if (isNighttime && !ClaimsAnalyser.HasNightPrivileges(user))
            {
                return null;
            }

            var newString = new StringEntity(value);
            _context.Strings.Add(newString);
            await _context.SaveChangesAsync();
            return new StringDto(newString.Id!.Value, newString.Value);
        }
    }
}

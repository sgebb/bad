using Microsoft.EntityFrameworkCore;

namespace Bad.Database
{
    public class BadDbContext : DbContext
    {
        public BadDbContext(DbContextOptions<BadDbContext> options) : base(options) {}

        public DbSet<StringEntity> Strings { get; set; }

        public DbSet<NumberEntity> Numbers { get; set; }
    }
}

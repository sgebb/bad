using Microsoft.EntityFrameworkCore;

namespace Bad.Database
{
    public class BadDbContext : DbContext
    {
        public BadDbContext(DbContextOptions<BadDbContext> options) : base(options) {}

        public DbSet<NumberEntity> Numbers { get; set; }

        // NOT RELEVANT YET
        public DbSet<StringEntity> Strings { get; set; }
    }
}

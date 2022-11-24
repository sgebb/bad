using Microsoft.EntityFrameworkCore;

namespace Bad.Database
{
    public class BadDbContext : DbContext
    {
        public BadDbContext(DbContextOptions<BadDbContext> options) : base(options) {}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DbSet<StringEntity> Strings { get; set; }

        public DbSet<NumberEntity> Numbers { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

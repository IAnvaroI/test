using Microsoft.EntityFrameworkCore;

namespace Lab7
{
    public class RecordContext : DbContext
    {
        public RecordContext(DbContextOptions<RecordContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Record> Records { get; set; }
    }
}

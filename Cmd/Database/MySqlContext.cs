using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cmd.Database {
    public class MySqlContext : DbContext {
        public MySqlContext(DbContextOptions options) : base(options) { }

        public LoggerFactory LoggerFactory { get; set; } = new LoggerFactory();

        public DbSet<MySqlIssue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MySqlIssue>().HasIndex(c => c.Key).IsUnique().HasName("Issues_Key");
        }
    }
}

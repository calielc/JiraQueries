using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cmd.Database {
    public class MySqlContextFactory : IDesignTimeDbContextFactory<MySqlContext> {
        public MySqlContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<MySqlContext>();
            optionsBuilder.UseMySqlConfiguration();

            return new MySqlContext(optionsBuilder.Options);
        }
    }
}
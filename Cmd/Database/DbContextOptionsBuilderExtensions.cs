using Microsoft.EntityFrameworkCore;

namespace Cmd.Database {
    public static class DbContextOptionsBuilderExtensions {
        public static void UseMySqlConfiguration(this DbContextOptionsBuilder self) {
            var config = ConnectionStringConfig.LoadFromResource();

            self.UseMySql(config.MySql);
        }
    }
}
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Cmd.Database {
    public static class DbContextOptionsBuilderExtensions {
        public static void UseMySqlConfiguration(this DbContextOptionsBuilder self) {
            var connectionString = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
            self.UseMySql(connectionString);
        }
    }
}
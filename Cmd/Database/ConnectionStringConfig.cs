using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Cmd.Database {
    public struct ConnectionStringConfig {
        public string MySql { get; set; }

        public static ConnectionStringConfig LoadFromResource() {
            var assembly = typeof(DbContextOptionsBuilderExtensions).Assembly;
            var resourceStream = assembly.GetManifestResourceStream("Cmd.Database.ConnectionString.json");

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                var fileContent = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ConnectionStringConfig>(fileContent);
            }
        }
    }
}
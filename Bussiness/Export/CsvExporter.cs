using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace JiraQueries.Bussiness.Export {
    public class CsvExporter<TItem> : ICsvExporter {
        public string Separator { get; set; } = ",";
        public IEnumerable<TItem> Items { get; set; }
        public IReadOnlyCollection<IMappingItem<TItem>> Map { get; set; }
        public CultureInfo Culture { get; set; } = CultureInfo.CurrentUICulture;

        public string Build() {
            var result = new StringBuilder();

            result.AppendLine(string.Join(Separator, Map.Select(map => CsvValue(map.Field))));
            foreach (var item in Items) {
                result.AppendLine(string.Join(Separator, Map.Select(map => CsvValue(map.Resolve(item, Culture)))));
            }

            return result.ToString();
        }

        private static string CsvValue(string value) => $"\"{value.Replace("\"", "")}\"";
    }
}
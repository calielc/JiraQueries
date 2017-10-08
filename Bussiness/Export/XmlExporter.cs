using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;

namespace JiraQueries.Bussiness.Export {
    public class XmlExporter<TItem> : IXmlExporter {
        public IEnumerable<TItem> Items { get; set; }
        public IReadOnlyCollection<IMappingItem<TItem>> Map { get; set; }
        public CultureInfo Culture { get; set; } = CultureInfo.CurrentUICulture;

        public string XsdUrl { get; set; }

        public string Build() {
            var result = new StringBuilder();
            result.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\" ?>");
            result.AppendLine();

            result.AppendLine(string.IsNullOrWhiteSpace(XsdUrl)
                ? "<items>"
                : $"<items xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"{XsdUrl}\"> ");

            foreach (var item in Items) {
                result.AppendLine("  <item>");
                foreach (var map in Map) {
                    var tag = map.Field;
                    var value = SecurityElement.Escape(map.Resolve(item, Culture));

                    result.AppendLine($"    <{tag}>{value}</{tag}>");
                }
                result.AppendLine("  </item>");
            }
            result.AppendLine("</items>");

            return result.ToString();
        }
    }
}


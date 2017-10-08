using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;

namespace JiraQueries.Bussiness.Export {
    public class XmlSpreedsheetExporter<TItem> : IXmlExporter {
        public IEnumerable<TItem> Items { get; set; }
        public IReadOnlyCollection<IMappingItem<TItem>> Map { get; set; }
        public CultureInfo Culture { get; set; } = CultureInfo.CurrentUICulture;

        public string Build() {
            var result = new StringBuilder();
            result.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
            result.AppendLine("<?mso-application progid=\"Excel.Sheet\"?>");

            result.AppendLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:html=\"http://www.w3.org/TR/REC-html40\" xmlns:x2=\"http://schemas.microsoft.com/office/excel/2003/xml\">");
            result.AppendLine("<Worksheet><Table>");

            result.AppendLine("<Row>");
            foreach (var map in Map) {
                result.AppendLine($"<Cell><Data>{map.Field}</Data></Cell>");
            }
            result.AppendLine("</Row>");

            foreach (var item in Items) {
                result.AppendLine("<Row>");
                foreach (var map in Map) {
                    var dataType = GetDataType(map.DataType);
                    var value = SecurityElement.Escape(map.Resolve(item, Culture));

                    result.AppendLine($"<Cell><Data ss:Type=\"{dataType}\">{value}</Data></Cell>");
                }
                result.AppendLine("</Row>");
            }
            result.AppendLine("</Table></Worksheet></Workbook>");

            return result.ToString();
        }

        private static string GetDataType(Type mapDataType) {
            var numberTypes = new[]
            {
                typeof(int),typeof(long),typeof(short),typeof(byte),typeof(uint),typeof(ulong),typeof(ushort),
                typeof(int?),typeof(long?),typeof(short?),typeof(byte?),typeof(uint?),typeof(ulong?),typeof(ushort?),
                typeof(float), typeof(double), typeof(decimal),
                typeof(float?), typeof(double?), typeof(decimal?),
            };
            if (numberTypes.Contains(mapDataType)) {
                return "Number";
            }

            return "String";
        }
    }
}
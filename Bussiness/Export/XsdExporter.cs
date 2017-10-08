using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JiraQueries.Bussiness.Export {
    public class XsdExporter : IXmlExporter {
        private static readonly Type[] NullableTypes = {
            typeof(byte?) ,
            typeof(decimal?),
            typeof(float?),
            typeof(double?),
            typeof(int?),
            typeof(long?),
            typeof(short?),
            typeof(uint?),
            typeof(ulong?),
            typeof(ushort?),
            typeof(DateTime?),
            typeof(TimeSpan?) };

        public IReadOnlyCollection<IMappingItem> Map { get; set; }

        public delegate void EventHandler(string field, Type type, ref string xsdType);

        public Func<(string field, Type dataType, string xsdType), string> AdjustDataType { get; set; }

        public string Build() {
            var result = new StringBuilder();
            result.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            result.AppendLine();
            result.AppendLine("<xs:schema attributeFormDefault=\"unqualified\" elementFormDefault=\"qualified\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">");
            result.AppendLine("  <xs:element name=\"items\">");
            result.AppendLine("    <xs:complexType>");
            result.AppendLine("      <xs:sequence>");
            result.AppendLine("        <xs:element name=\"item\" maxOccurs=\"unbounded\" minOccurs=\"0\">");
            result.AppendLine("          <xs:complexType>");
            result.AppendLine("            <xs:sequence>");
            foreach (var map in Map) {
                var dataType = GetDataType(map.DataType);
                if (AdjustDataType != null) {
                    dataType = AdjustDataType((map.Field, map.DataType, dataType));
                }

                var isNullable = Enumerable.Contains<Type>(NullableTypes, map.DataType);

                var nillable = isNullable ? "nillable=\"true\" " : "";
                result.AppendLine($"               <xs:element name=\"{map.Field}\" type=\"{dataType}\" {nillable}/>");
            }
            result.AppendLine("            </xs:sequence>");
            result.AppendLine("          </xs:complexType>");
            result.AppendLine("        </xs:element>");
            result.AppendLine("      </xs:sequence>");
            result.AppendLine("    </xs:complexType>");
            result.AppendLine("  </xs:element>");
            result.AppendLine("</xs:schema>");
            return result.ToString();
        }

        private static string GetDataType(Type mapDataType) {
            if (new[] { typeof(byte), typeof(byte?) }.Contains(mapDataType)) {
                return "xs:byte";
            }
            if (new[] { typeof(decimal), typeof(decimal?), typeof(float), typeof(float?), typeof(double), typeof(double?) }.Contains(mapDataType)) {
                return "xs:decimal";
            }
            if (new[] { typeof(int), typeof(int?) }.Contains(mapDataType)) {
                return "xs:int";
            }
            if (new[] { typeof(long), typeof(long?) }.Contains(mapDataType)) {
                return "xs:long";
            }
            if (new[] { typeof(short), typeof(short?) }.Contains(mapDataType)) {
                return "xs:short";
            }
            if (new[] { typeof(uint), typeof(uint?) }.Contains(mapDataType)) {
                return "xs:unsignedInt";
            }
            if (new[] { typeof(ulong), typeof(ulong?) }.Contains(mapDataType)) {
                return "xs:unsignedLong";
            }
            if (new[] { typeof(ushort), typeof(ushort?) }.Contains(mapDataType)) {
                return "xs:unsignedShort";
            }
            if (new[] { typeof(DateTime), typeof(DateTime?) }.Contains(mapDataType)) {
                return "xs:dateTime";
            }
            if (new[] { typeof(TimeSpan), typeof(TimeSpan?) }.Contains(mapDataType)) {
                return "xs:time";
            }

            return "xs:string";
        }
    }
}
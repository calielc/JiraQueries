using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using JiraQueries.Bussiness.Export;

namespace JiraQueries.Bussiness.Services {
    public abstract class Service<T> : IService<T> {
        public string Project { get; set; }
        public string XsdUrl { get; set; }

        public abstract MappingBuilder<T> FieldsMap { get; }

        public virtual async Task<IXmlExporter> Xml(string xsdUrl) => new XmlExporter<T> {
            Culture = new CultureInfo("en-US"),
            Items = await Load(),
            Map = FieldsMap,
            XsdUrl = XsdUrl
        };

        public virtual IXmlExporter Xsd() => new XsdExporter {
            Map = FieldsMap
        };

        public virtual async Task<ICsvExporter> Csv(string separator) => new CsvExporter<T> {
            Culture = new CultureInfo("en-US"),
            Items = await Load(),
            Map = FieldsMap,
            Separator = separator
        };

        public virtual async Task<IEnumerable<T>> Json() => await Load();

        protected abstract Task<IEnumerable<T>> Load();
    }
}
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using JiraQueries.Bussiness.Export;

namespace JiraQueries.Bussiness.Services {
    public abstract class Service<T> : IService<T> {
        private static readonly CultureInfo Culture = new CultureInfo("en-US");

        public string Project { get; set; }
        public string XsdUrl { get; set; }

        public abstract MappingBuilder<T> FieldsMap { get; }

        public virtual async Task<IXmlExporter> Xml(string xsdUrl) => new XmlExporter<T> {
            Culture = Culture,
            Items = await Load(),
            Map = FieldsMap,
            XsdUrl = XsdUrl
        };

        public virtual IXmlExporter Xsd() => new XsdExporter {
            Map = FieldsMap
        };

        public virtual async Task<ICsvExporter> Csv(string separator) => new CsvExporter<T> {
            Culture = Culture,
            Items = await Load(),
            Map = FieldsMap,
            Separator = separator
        };

        public virtual async Task<IEnumerable<T>> Raw() => await Load();

        protected abstract Task<IEnumerable<T>> Load();
    }
}
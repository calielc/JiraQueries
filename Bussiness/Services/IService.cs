using System.Collections.Generic;
using System.Threading.Tasks;
using JiraQueries.Bussiness.Export;

namespace JiraQueries.Bussiness.Services {
    public interface IService<T> {
        string Project { get; }
        MappingBuilder<T> FieldsMap { get; }

        Task<IXmlExporter> Xml(string xsdUrl);
        IXmlExporter Xsd();

        Task<ICsvExporter> Csv(string separator);

        Task<IEnumerable<T>> Raw();
    }
}
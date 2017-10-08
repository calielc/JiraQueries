namespace JiraQueries.Bussiness.Export {
    public interface IExporter {
        string Build();
    }
    public interface IXmlExporter : IExporter { }

    public interface ICsvExporter : IExporter { }
}
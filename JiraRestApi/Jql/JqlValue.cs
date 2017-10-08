namespace JiraQueries.JiraRestApi.Jql {
    public interface IJqlValue : IUrlResolver { }

    public abstract class JqlValue<T> : IJqlValue {
        public abstract string Resolve();
    }
}
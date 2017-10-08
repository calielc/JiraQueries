using System.Net.Http.Headers;

namespace JiraQueries.JiraRestApi {
    public interface IJiraAPiClientConfig {
        string Url { get; }
        AuthenticationHeaderValue Authorization { get; }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using JiraQueries.JiraRestApi.Jql;
using JiraQueries.JiraRestApi.Models;

namespace JiraQueries.JiraRestApi {
    public interface IJiraApiClient {
        Task<IReadOnlyCollection<JiraIssue>> Search(IJql jql, JqlExpand[] expands);
    }
}
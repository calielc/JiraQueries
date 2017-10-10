using JiraQueries.Bussiness.Jira;
using JiraQueries.Bussiness.Models;
using JiraQueries.Bussiness.Services;

namespace JiraQueries.Api.Controllers {
    public sealed class AllTypesAndDoneOrRejectedController : ServiceController<IssueViewModel> {
        protected override IService<IssueViewModel> CreateService(string project)
            => new AllTypesAndDoneOrRejectedService(JiraAccessPoint.Instance) {
                Project = project
            };
    }
}
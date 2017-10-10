using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiraQueries.Bussiness.Export;
using JiraQueries.Bussiness.Jira;
using JiraQueries.Bussiness.Models;
using JiraQueries.JiraRestApi.Jql;

namespace JiraQueries.Bussiness.Services {
    public sealed class AllTypesAndDoneOrRejectedService : Service<IssueViewModel> {
        private readonly CachedJiraSearch _searcher;

        public AllTypesAndDoneOrRejectedService(JiraAccessPoint accessPoint) {
            _searcher = accessPoint.CachedSearch;
        }

        public override MappingBuilder<IssueViewModel> FieldsMap => new MappingBuilder<IssueViewModel>()
            .Add("date", item => item.Milestones.ResolvedAt.Date, "yyyy-MM-dd")
            .Add("dateYear", item => item.Milestones.ResolvedAt.Year)
            .Add("dateMonth", item => item.Milestones.ResolvedAt.Month.Value)
            .Add("dateQuarter", item => item.Milestones.ResolvedAt.Quarter.Value)
            .Add("dateDayOfWeek", item => (int) item.Milestones.ResolvedAt.Week.DayValue)

            .Add("projectKey", item => item.Project.Key)
            .Add("projectName", item => item.Project.Name)

            .Add("key", item => item.Key)
            .Add("title", item => item.Title)
            .Add("name", item => $"({item.Key}) {item.Title}")

            .Add("type", item => item.IssueType)
            .Add("status", item => item.Status)

            .Add("release", item => item.Sprint?.Release)
            .Add("sprint", item => item.Sprint?.Name)

            .Add("epic", item => item.Epic)
            .Add("subtasks", item => item.SubTasks?.Count)

            .Add("assigneeKey", item => item.Assignee?.Key)
            .Add("assigneeName", item => item.Assignee?.Name)
            .Add("assigneeShortName", item => CreateShortName(item?.Assignee?.Name))

            .Add("reviewerKey", item => item.Reviewer?.Key)
            .Add("reviewerName", item => item.Reviewer?.Name)
            .Add("reviewerShortName", item => CreateShortName(item?.Reviewer?.Name))

            .Add("storypoints", item => item.StoryPoints)

            .Add("hoursSpent", item => item.TimeSpent?.TotalIncludingSubstask?.TotalHours, "0.00")
            .Add("hoursSpentPerAssignee", item => item.TimeSpent?.Assignee?.TotalHours, "0.00")
            .Add("hoursSpentPerAssigneePerc", item => item.TimeSpent?.AssigneePercentual, "0.00")
            .Add("hoursSpentPerReviewer", item => item.TimeSpent?.Reviewer?.TotalHours, "0.00")
            .Add("hoursSpentPerReviewerPerc", item => item.TimeSpent?.ReviewerPercentual, "0.00")
            .Add("hoursSpentPerOthers", item => item.TimeSpent?.Others?.TotalHours, "0.00")

            .Add("daysToStart", item => item.Milestones.TimeToStart?.TotalDays, "0.00")
            .Add("daysToReject", item => item.Milestones.TimeToReject?.TotalDays, "0.00")
            .Add("daysToDone", item => item.Milestones.TimeToDone?.TotalDays, "0.00")
            .Add("daysToResolve", item => item.Milestones.TimeToResolve?.TotalDays, "0.00")

            .Add("daysInProgress", item => item.Milestones.TimeInProgress?.TotalDays, "0.00")
            .Add("daysInPullRequest", item => item.Milestones.TimeInPullRequest?.TotalDays, "0.00")
            .Add("daysInTest", item => item.Milestones.TimeInTest?.TotalDays, "0.00")
            .Add("daysInImpediment", item => item.Milestones.TimeInImpediment?.TotalDays, "0.00")
            .Add("daysInDevelopment", item => item.Milestones.TimeInDevelopment?.TotalDays, "0.00")

            .Add("productionFactor", item => item.CustomFields.FatorProdutizacao)
            .Add("technologyUpdateFactor", item => item.CustomFields.FatorAtualizacaoTecnologica)
            .Add("implementerFunding", item => item.CustomFields.FinanciadorImplementacao)
            .Add("bugCause", item => item.CustomFields.CausaRaiz)
            .Add("bugSource", item => item.CustomFields.Fonte)
            .Add("serviceDesk", item => {
                if (item.CustomFields.Chamado is null) {
                    return null;
                }
                if (item.CustomFields.Chamado) {
                    return "INC";
                }
                return "No";
            });

        private static string CreateShortName(string name) {
            if (name is null) {
                return null;
            }

            var indexFirstSpace = name.IndexOf(" ", StringComparison.Ordinal);
            return indexFirstSpace >= 0 ? name.Substring(0, indexFirstSpace) : name;
        }

        protected override async Task<IEnumerable<IssueViewModel>> Load() {
            var jql = Jql.And(
                JqlField.Project.Equal(Project),
                JqlField.Status.In(JqlStatus.Done, JqlStatus.Rejected),
                JqlField.IssueType.In(JqlIssueType.Bug, JqlIssueType.Story, JqlIssueType.Task, JqlIssueType.SubTask)
            );

            var issues = await _searcher.SplitByResolvedField(jql, new[] { JqlExpand.ChangeLog });

            return issues.Select(issue => new IssueViewModel(issue)).OrderBy(issue => issue.Milestones.ResolvedAt.Value);
        }
    }
}
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

            .Add("key", item => item.Key)
            .Add("title", item => item.Summary)

            .Add("type", item => item.IssueType)
            .Add("status", item => item.Status)

            .Add("release", item => item.Sprint?.Release)
            .Add("sprint", item => item.Sprint?.Name)

            .Add("epic", item => item.Epic)
            .Add("subtasks", item => item.SubTasks?.Count)
            .Add("hasSubtasks", item => item.SubTasks?.Any?.Value)

            .Add("assignee", item => item.Assignee)
            .Add("reviewer", item => item.Reviewer)

            .Add("storypoints", item => item.StoryPoints)
            .Add("hoursSpent", item => item.AggregateTimespent?.TotalHours, "0.00")

            .Add("daysToStart", item => item.Milestones.TimeToStart?.TotalDays, "0.00")
            .Add("daysToReject", item => item.Milestones.TimeToReject?.TotalDays, "0.00")
            .Add("daysToDone", item => item.Milestones.TimeToDone?.TotalDays, "0.00")
            .Add("daysToResolve", item => item.Milestones.TimeToResolve?.TotalDays, "0.00")

            .Add("daysInProgress", item => item.Milestones.TimeInProgress?.TotalDays, "0.00")
            .Add("daysInPullRequest", item => item.Milestones.TimeInPullRequest?.TotalDays, "0.00")
            .Add("daysInTest", item => item.Milestones.TimeInTest?.TotalDays, "0.00")
            .Add("daysInDevelopment", item => item.Milestones.TimeInDevelopment?.TotalDays, "0.00")

            .Add("productionFactor", item => item.CustomFields.FatorProdutizacao)
            .Add("technologyUpdateFactor", item => item.CustomFields.FatorAtualizacaoTecnologica)
            .Add("implementerFunding", item => item.CustomFields.FinanciadorImplementacao)
            .Add("bugCause", item => item.CustomFields.CausaRaiz)
            .Add("bugSource", item => item.CustomFields.Fonte)
            .Add("serviceDesk", item => item.CustomFields.Chamado?.Text)
        ;

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
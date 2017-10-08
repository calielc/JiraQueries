using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiraQueries.Bussiness.Export;
using JiraQueries.Bussiness.Jira;
using JiraQueries.Bussiness.Models;
using JiraQueries.JiraRestApi.Jql;

namespace JiraQueries.Bussiness.Services {
    [Obsolete]
    public sealed class TimelineService : Service<IssueViewModel> {
        private readonly CachedJiraSearch _searcher;

        public TimelineService(JiraAccessPoint accessPoint) {
            _searcher = accessPoint.CachedSearch;
        }

        public override MappingBuilder<IssueViewModel> FieldsMap => DefaultMappingBuilder.ForResolvedIssues
            .Add("type", item => {
                if (item.SubTasks) {
                    return "Parent " + item.IssueType;
                }
                return item.IssueType;
            })
            .Add("status", item => item.Status)

            .Add("daysToStart", item => item.Milestones.TimeToStart?.TotalDays, "0.00")
            .Add("daysToReject", item => item.Milestones.TimeToReject?.TotalDays, "0.00")
            .Add("daysToDone", item => item.Milestones.TimeToDone?.TotalDays, "0.00")
            .Add("daysToResolve", item => item.Milestones.TimeToResolve?.TotalDays, "0.00")

            .Add("daysInProgress", item => item.Milestones.TimeInProgress?.TotalDays, "0.00")
            .Add("daysInPullRequest", item => item.Milestones.TimeInPullRequest?.TotalDays, "0.00")
            .Add("daysInTest", item => item.Milestones.TimeInTest?.TotalDays, "0.00")
            .Add("daysInDevelopment", item => item.Milestones.TimeInDevelopment?.TotalDays, "0.00");

        public override IXmlExporter Xsd() => new XsdExporter {
            AdjustDataType = input => input.field == "date" ? "xs:date" : input.xsdType,
            Map = FieldsMap,
        };

        protected override async Task<IEnumerable<IssueViewModel>> Load() {
            var jql = Jql.And(
                JqlField.Project.Equal(Project),
                JqlField.Status.In(JqlStatus.Done, JqlStatus.Rejected),
                JqlField.IssueType.In(JqlIssueType.Bug, JqlIssueType.Story, JqlIssueType.Task, JqlIssueType.SubTask)
            );

            var issues = await _searcher.SplitByResolvedField(jql, new[] { JqlExpand.ChangeLog });

            return issues.Select(issue => new IssueViewModel(issue)).OrderBy(x => x.Milestones.ResolvedAt?.Value);
        }
    }
}
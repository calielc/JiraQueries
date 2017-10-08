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
    public sealed class CampaignService : Service<IssueViewModel> {
        private readonly CachedJiraSearch _searcher;

        public CampaignService(JiraAccessPoint accessPoint) {
            _searcher = accessPoint.CachedSearch;
        }

        public override MappingBuilder<IssueViewModel> FieldsMap => DefaultMappingBuilder.ForResolvedIssues;

        public override IXmlExporter Xsd() => new XsdExporter {
            AdjustDataType = input => input.field == "date" ? "xs:date" : input.xsdType,
            Map = FieldsMap,
        };

        protected override async Task<IEnumerable<IssueViewModel>> Load() {
            var jql = Jql.And(
                JqlField.Project.Equal(Project),
                JqlField.Status.In(JqlStatus.Done),
                JqlField.IssueType.In(JqlIssueType.Bug, JqlIssueType.Story, JqlIssueType.Task)
            );

            var issues = await _searcher.SplitByResolvedField(jql, new[] { JqlExpand.ChangeLog });

            return issues.Select(issue => new IssueViewModel(issue)).OrderBy(x => x.Milestones.ResolvedAt?.Value);
        }
    }
}
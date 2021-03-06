﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiraQueries.Bussiness.Jira;
using JiraQueries.Bussiness.Models;
using JiraQueries.JiraRestApi.Jql;

namespace JiraQueries.Bussiness.Services {
    public sealed class AllTypesAndDoneOrRejectedService : IJiraService {
        private readonly CachedJiraSearch _searcher;

        public AllTypesAndDoneOrRejectedService(JiraAccessPoint accessPoint) {
            _searcher = accessPoint.CachedSearch;
        }

        public string Project { get; set; }

        public async Task<IEnumerable<IssueViewModel>> Load() {
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
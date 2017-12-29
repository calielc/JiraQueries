using System;
using System.Linq;
using System.Text.RegularExpressions;
using JiraQueries.JiraRestApi.Models;

namespace JiraQueries.Bussiness.Models {
    public sealed class IssueViewModel {
        private readonly JiraIssue _issue;

        public IssueViewModel(JiraIssue issue) => _issue = issue;

        public CommonViewModel Project => _issue.Fields.Project;

        public string Key => _issue.Key;

        public string Title => _issue.Fields.Summary;

        public string IssueType => _issue.Fields.IssueType;

        public CustomFieldsViewModel CustomFields => new CustomFieldsViewModel(_issue.Fields);

        public LabelsViewModel Labels => _issue.Fields.Labels;

        public CommonViewModel Assignee => _issue.Fields.Assignee;

        public CommonViewModel Reviewer => _issue.Fields.Reviewer;

        public string Epic => _issue.Fields.Epic;

        public ArrayViewModel SubTasks => _issue.Fields.SubTasks;

        public SprintViewModel Sprint => _issue.Fields.Sprints?.FirstOrDefault();

        public string AffectsShortVersion => IdentifyVersion(_issue.Fields, 2, _issue.Fields.AffectsVersions);
        public string AffectsFullVersion => IdentifyVersion(_issue.Fields, 3, _issue.Fields.AffectsVersions);

        public string FixShortVersion => IdentifyVersion(_issue.Fields, 2, _issue.Fields.FixVersions);
        public string FixFullVersion => IdentifyVersion(_issue.Fields, 3, _issue.Fields.FixVersions);

        public int? StoryPoints => CalcStory();

        public string Status => _issue.Fields.Status;

        public MilestonesViewModel Milestones => _issue;

        public TimespentViewModel TimeSpent => _issue;

        private int? CalcStory() {
            if (IssueType == "Sub-task" && _issue.Fields.StoryPoints is null) {
                return TryIdentifyStoryPoints(_issue.Fields.Summary);
            }

            if (_issue.Fields.StoryPoints is null) {
                return null;
            }

            return Convert.ToInt32(_issue.Fields.StoryPoints);

            int? TryIdentifyStoryPoints(string summary) {
                var expressions = new[]
                {
                    new Regex(@"[\[\(](\d*)\s?sp[-\s\w]*[\]\)]", RegexOptions.IgnoreCase),
                    new Regex(@"^(\d*)\s?sp[-\s\w]*", RegexOptions.IgnoreCase),
                };

                foreach (var expression in expressions) {
                    var value = expression.Match(summary).Groups[1].Value;
                    if (int.TryParse(value, out var result)) {
                        return result;
                    }
                }

                return null;
            }
        }

        private static string IdentifyVersion(JiraIssueField issueFields, int maxParts, JiraCommon[] versions) {
            if (issueFields.IssueType != JiraConsts.IssueTypeBug) {
                return null;
            }

            if (versions == null || versions.Any() == false) {
                return Resource.Unspecified;
            }

            var version = versions.Select(item => (string) item).OrderBy(text => text).First();
            var parts = version.Split(".").Take(maxParts).ToList();
            while (parts.Count < maxParts) {
                parts.Add("0");
            }

            return string.Join(".", parts);
        }
    }
}
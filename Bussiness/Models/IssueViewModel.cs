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

        public string FixVersions => _issue.Fields.FixVersions == null
            ? default
            : string.Join(", ", _issue.Fields.FixVersions.Select(fixVersion => (string) fixVersion));

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
    }
}
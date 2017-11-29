using System.Linq;
using JiraQueries.JiraRestApi.Models;

namespace JiraQueries.Bussiness.Models {
    public sealed class TimespentViewModel {
        private TimespentViewModel(JiraIssue issue) {
            Total = issue.Fields.Timespent;
            TotalIncludingSubstask = issue.Fields.AggregateTimespent;

            var timespentPerAuthor = issue.ChangeLog.Histories
                .SelectMany(history => history.Items.Where(y => y.Field == "timespent").Select(y => new Item(history, y)))
                .GroupBy(item => item.Author)
                .ToDictionary(group => group.Key, group => group.Sum(y => y.DurationInSeconds));

            var assignee = issue.Fields.Assignee?.Key;
            if (assignee != null) {
                long? timespent = timespentPerAuthor.ContainsKey(assignee) ? timespentPerAuthor[assignee] : default;

                Assignee = timespent;
                AssigneePercentual = CalcPercentual(timespent);
            }

            var reviewer = issue.Fields.Reviewer?.Key;
            if (reviewer != null) {
                long? timespent = timespentPerAuthor.ContainsKey(reviewer) ? timespentPerAuthor[reviewer] : default;

                Reviewer = timespent;
                ReviewerPercentual = CalcPercentual(timespent);
            }

            var othersTimespent = timespentPerAuthor.Where(pair => pair.Key != assignee && pair.Key != reviewer).Sum(pair => pair.Value);
            Others = othersTimespent;
            OthersPercentual = CalcPercentual(othersTimespent);

            double? CalcPercentual(double? value) {
                if (value is null || issue.Fields.Timespent is null || issue.Fields.Timespent == 0) {
                    return default;
                }
                return value / issue.Fields.Timespent;
            }
        }


        public TimeSpanViewModel Assignee { get; }
        public double? AssigneePercentual { get; }

        public TimeSpanViewModel Reviewer { get; }
        public double? ReviewerPercentual { get; }

        public TimeSpanViewModel Others { get; }
        public double? OthersPercentual { get; }

        public TimeSpanViewModel Total { get; }
        public TimeSpanViewModel TotalIncludingSubstask { get; }

        private class Item {
            public Item(JiraHistory jiraHistory, JiraHistoryItem jiraHistoryItem) {
                Author = jiraHistory.Author.Key;

                var from = string.IsNullOrWhiteSpace(jiraHistoryItem.FromValue) ? 0L : long.Parse(jiraHistoryItem.FromValue);
                var to = string.IsNullOrWhiteSpace(jiraHistoryItem.ToValue) ? 0L : long.Parse(jiraHistoryItem.ToValue);
                DurationInSeconds = to - @from;
            }

            public string Author { get; }

            public long DurationInSeconds { get; }
        }

        public static implicit operator TimespentViewModel(JiraIssue issue)
            => issue is null ? null : new TimespentViewModel(issue);
    }
}
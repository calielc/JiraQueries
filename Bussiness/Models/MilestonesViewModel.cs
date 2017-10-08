using System;
using System.Linq;
using JiraQueries.JiraRestApi.Models;

namespace JiraQueries.Bussiness.Models {
    public sealed class MilestonesViewModel {
        private readonly DateTime? _inProgressAt;
        private readonly DateTime? _pullRequestAt;
        private readonly DateTime? _testAt;
        private readonly DateTime _createdAt;
        private readonly DateTime? _rejectedAt;
        private readonly DateTime? _doneAt;
        private readonly DateTime? _resolvedAt;

        private MilestonesViewModel(JiraIssue issue) {
            var histories = issue.ChangeLog?.Histories
                .SelectMany(history => history.Items.Where(historyItem => historyItem.Field == "status").Select(historyItem => new Item(history, historyItem)))
                .ToArray()
                ?? new Item[0];

            _createdAt = issue.Fields.Created;
            _inProgressAt = histories.LastOrDefault(item => item.To == "In Progress")?.Created;
            _pullRequestAt = histories.LastOrDefault(item => item.To == "Pull Request")?.Created;
            _testAt = histories.LastOrDefault(item => item.To == "Test")?.Created;
            _rejectedAt = issue.Fields.Status == "Rejected" ? histories.LastOrDefault(item => item.To == "Rejected")?.Created : default;
            _doneAt = issue.Fields.Status == "Done" ? histories.LastOrDefault(item => item.To == "Done")?.Created : default;
            _resolvedAt = _doneAt ?? _rejectedAt;
        }

        public DateTimeViewModel CreatedAt => _createdAt;
        public DateTimeViewModel InProgressAt => _inProgressAt;
        public DateTimeViewModel PullRequestAt => _pullRequestAt;
        public DateTimeViewModel TestAt => _testAt;
        public DateTimeViewModel RejectedAt => _rejectedAt;
        public DateTimeViewModel DoneAt => _doneAt;
        public DateTimeViewModel ResolvedAt => _resolvedAt;

        public TimeSpanViewModel TimeToStart => CalcTimeSpan(_createdAt, _inProgressAt, _pullRequestAt, _testAt, _resolvedAt);
        public TimeSpanViewModel TimeToReject => CalcTimeSpan(_createdAt, _rejectedAt);
        public TimeSpanViewModel TimeToDone => CalcTimeSpan(_createdAt, _doneAt);
        public TimeSpanViewModel TimeToResolve => CalcTimeSpan(_createdAt, _resolvedAt);

        public TimeSpanViewModel TimeInProgress => CalcTimeSpan(_inProgressAt, _pullRequestAt, _testAt, _resolvedAt);
        public TimeSpanViewModel TimeInPullRequest => CalcTimeSpan(_pullRequestAt, _testAt, _resolvedAt);
        public TimeSpanViewModel TimeInTest => CalcTimeSpan(_testAt, _resolvedAt);
        public TimeSpanViewModel TimeInDevelopment => CalcTimeSpan(_inProgressAt, _resolvedAt);

        public static implicit operator MilestonesViewModel(JiraIssue issue) => new MilestonesViewModel(issue);

        private static TimeSpan? CalcTimeSpan(DateTime? before, params DateTime?[] afters) {
            if (before is null) {
                return default;
            }

            var valids = afters.Where(item => item != null).Select(item => item.Value).ToArray();
            if (valids.Length == 0) {
                return default;
            }

            var after = valids.Min();
            if (after < before.Value) {
                return default;
            }

            return after - before.Value;
        }

        private class Item {
            public Item(JiraHistory history, JiraHistoryItem historyItem) {
                Created = history.Created;
                To = historyItem.ToText ?? historyItem.ToValue;
            }

            public DateTime Created { get; }
            public string To { get; }
        }
    }
}
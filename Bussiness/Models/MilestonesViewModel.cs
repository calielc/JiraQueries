using System;
using System.Collections.Generic;
using System.Linq;
using JiraQueries.JiraRestApi.Models;

namespace JiraQueries.Bussiness.Models {
    public sealed class MilestonesViewModel {
        private const string StatusInProgress = "In Progress";
        private const string StatusPullRequest = "Pull Request";
        private const string StatusTest = "Test";
        private const string StatusRejected = "Rejected";
        private const string StatusDone = "Done";
        private const string FlaggedImpediment = "Impediment";

        private readonly Item[] _statusCollection;
        private readonly Item[] _flaggedCollection;

        private readonly DateTime _createdAt;
        private readonly DateTime? _inProgressAt;
        private readonly DateTime? _pullRequestAt;
        private readonly DateTime? _testAt;
        private readonly DateTime? _doneAt;
        private readonly DateTime? _rejectedAt;
        private readonly DateTime? _resolvedAt;

        private readonly TimeSpan? _timeInProgress;
        private readonly TimeSpan? _timeInPullRequest;
        private readonly TimeSpan? _timeInInTest;

        private MilestonesViewModel(JiraIssue issue) {
            _createdAt = issue.Fields.Created;

            _statusCollection = CreateCollection("status");
            _flaggedCollection = CreateCollection("Flagged");

            _inProgressAt = _statusCollection.LastOrDefault(item => item.To == StatusInProgress)?.When;
            _pullRequestAt = _statusCollection.LastOrDefault(item => item.To == StatusPullRequest)?.When;
            _testAt = _statusCollection.LastOrDefault(item => item.To == StatusTest)?.When;
            _rejectedAt = issue.Fields.Status == StatusRejected ? _statusCollection.LastOrDefault(item => item.To == StatusRejected)?.When : default;
            _doneAt = issue.Fields.Status == StatusDone ? _statusCollection.LastOrDefault(item => item.To == StatusDone)?.When : default;
            _resolvedAt = _doneAt ?? _rejectedAt;

            _timeInProgress = CalcTimeElapse(_statusCollection, StatusInProgress);
            _timeInPullRequest = CalcTimeElapse(_statusCollection, StatusPullRequest);
            _timeInInTest = CalcTimeElapse(_statusCollection, StatusTest);

            Item[] CreateCollection(string field) {
                if (issue.ChangeLog is null || issue.ChangeLog.Histories is null) {
                    return new Item[0];
                }

                return issue.ChangeLog.Histories
                    .SelectMany(history => history.Items.Where(item => item.Field == field).Select(item => new Item(history, item)))
                    .ToArray();
            }
        }

        public DateTimeViewModel CreatedAt => _createdAt;

        public DateTimeViewModel InProgressAt => _inProgressAt;
        public DateTimeViewModel PullRequestAt => _pullRequestAt;
        public DateTimeViewModel TestAt => _testAt;

        public DateTimeViewModel RejectedAt => _rejectedAt;
        public DateTimeViewModel DoneAt => _doneAt;
        public DateTimeViewModel ResolvedAt => _resolvedAt;

        public TimeSpanViewModel TimeInProgress => _timeInProgress;
        public TimeSpanViewModel TimeInPullRequest => _timeInPullRequest;
        public TimeSpanViewModel TimeInTest => _timeInInTest;

        public TimeSpanViewModel TimeInImpediment => CalcTimeElapse(_flaggedCollection, FlaggedImpediment);

        public TimeSpanViewModel TimeInDevelopment {
            get {
                if (_timeInProgress is null && _timeInPullRequest is null && _timeInInTest is null) {
                    return null;
                }

                return (_timeInProgress ?? TimeSpan.Zero) + (_timeInPullRequest ?? TimeSpan.Zero) + (_timeInInTest ?? TimeSpan.Zero);
            }
        }

        public TimeSpanViewModel TimeToStart {
            get {
                if (_statusCollection is null || _statusCollection.Length == 0) {
                    return null;
                }

                var firstResponse = _statusCollection.FirstOrDefault(item
                    => item.To == StatusInProgress
                    || item.To == StatusPullRequest
                    || item.To == StatusTest
                    || item.To == StatusDone
                    || item.To == StatusRejected
                );

                if (firstResponse is null) {
                    return null;
                }

                return firstResponse.When - _createdAt;
            }
        }
        public TimeSpanViewModel TimeToReject => _rejectedAt - _createdAt;
        public TimeSpanViewModel TimeToDone => _doneAt - _createdAt;
        public TimeSpanViewModel TimeToResolve => _resolvedAt - _createdAt;

        public static implicit operator MilestonesViewModel(JiraIssue issue) => new MilestonesViewModel(issue);

        private static TimeSpan? CalcTimeElapse(IReadOnlyList<Item> histories, string value) {
            if (histories is null || histories.Count == 0) {
                return null;
            }

            TimeSpan? result = default;
            for (var idx = 0; idx < histories.Count; idx++) {
                var start = histories[idx];
                if (start.To != value) {
                    continue;
                }

                var end = histories.Skip(idx).FirstOrDefault(x => x.From == value);
                if (end == null) {
                    continue;
                }

                if (result is null) {
                    result = end.When - start.When;
                }
                else {
                    result += end.When - start.When;
                }
            }

            if (result is null) {
                return null;
            }
            return result.Value;
        }

        private class Item {
            public Item(JiraHistory history, JiraHistoryItem historyItem) {
                When = history.Created;
                From = historyItem.FromText ?? historyItem.FromValue;
                To = historyItem.ToText ?? historyItem.ToValue;
            }

            public DateTime When { get; }

            public string From { get; }
            public string To { get; }
        }
    }
}
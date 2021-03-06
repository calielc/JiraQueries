﻿using System;
using JiraQueries.Bussiness.Models;

namespace Cmd.Database {
    public class MySqlIssue : ISimpleIssueSettable {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int DateYear { get; set; }
        public int DateMonth { get; set; }
        public int DateQuarter { get; set; }
        public int DateDayOfWeek { get; set; }

        public string ProjectKey { get; set; }
        public string ProjectName { get; set; }

        public string Key { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }
        public string Status { get; set; }
        public string Epic { get; set; }
        public int? Subtasks { get; set; }
        public int? Storypoints { get; set; }

        public string SprintRelease { get; set; }
        public string Sprint { get; set; }

        public string AssigneeKey { get; set; }
        public string AssigneeName { get; set; }
        public string AssigneeShortName { get; set; }

        public string ReviewerKey { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerShortName { get; set; }

        public double? HoursSpent { get; set; }
        public double? HoursSpentPerAssignee { get; set; }
        public double? HoursSpentPerAssigneePerc { get; set; }
        public double? HoursSpentPerReviewer { get; set; }
        public double? HoursSpentPerReviewerPerc { get; set; }
        public double? HoursSpentPerOthers { get; set; }
        public double? HoursSpentPerOthersPerc { get; set; }

        public double? DaysToStart { get; set; }
        public double? DaysToReject { get; set; }
        public double? DaysToDone { get; set; }
        public double? DaysToResolve { get; set; }

        public double? DaysInProgress { get; set; }
        public double? DaysInPullRequest { get; set; }
        public double? DaysInTest { get; set; }
        public double? DaysInImpediment { get; set; }
        public double? DaysInDevelopment { get; set; }

        public string Productization { get; set; }
        public double? ProductizationFactor { get; set; }
        public double? ProductizationHours { get; set; }

        public string TechnologyUpdate { get; set; }
        public double? TechnologyUpdateFactor { get; set; }
        public double? TechnologyUpdateHours { get; set; }

        public string ImplementerFunding { get; set; }

        public string BugCause { get; set; }
        public string BugSource { get; set; }
        public string ServiceDesk { get; set; }
        public string AffectsShortVersion { get; set; }
        public string AffectsFullVersion { get; set; }
        public string FixShortVersion { get; set; }
        public string FixFullVersion { get; set; }

        public string LabelBacklog { get; set; }
        public string LabelNaoPlanejado { get; set; }
    }
}
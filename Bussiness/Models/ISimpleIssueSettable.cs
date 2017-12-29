using System;

namespace JiraQueries.Bussiness.Models {
    public interface ISimpleIssueSettable {
        DateTime Date { set; }
        int DateYear { set; }
        int DateMonth { set; }
        int DateQuarter { set; }
        int DateDayOfWeek { set; }

        string ProjectKey { set; }
        string ProjectName { set; }

        string Key { set; }
        string Title { set; }
        string Name { set; }

        string Type { set; }
        string Status { set; }
        string Epic { set; }
        int? Subtasks { set; }
        int? Storypoints { set; }

        string SprintRelease { set; }
        string Sprint { set; }

        string AssigneeKey { set; }
        string AssigneeName { set; }
        string AssigneeShortName { set; }

        string ReviewerKey { set; }
        string ReviewerName { set; }
        string ReviewerShortName { set; }

        double? HoursSpent { set; }
        double? HoursSpentPerAssignee { set; }
        double? HoursSpentPerAssigneePerc { set; }
        double? HoursSpentPerReviewer { set; }
        double? HoursSpentPerReviewerPerc { set; }
        double? HoursSpentPerOthers { set; }
        double? HoursSpentPerOthersPerc { set; }

        double? DaysToStart { set; }
        double? DaysToReject { set; }
        double? DaysToDone { set; }
        double? DaysToResolve { set; }

        double? DaysInProgress { set; }
        double? DaysInPullRequest { set; }
        double? DaysInTest { set; }
        double? DaysInImpediment { set; }
        double? DaysInDevelopment { set; }

        string Productization { set; }
        double? ProductizationFactor { set; }
        double? ProductizationHours { set; }

        string TechnologyUpdate { set; }
        double? TechnologyUpdateFactor { set; }
        double? TechnologyUpdateHours { set; }

        string ImplementerFunding { set; }

        string BugCause { set; }
        string BugSource { set; }
        string ServiceDesk { set; }
        string AffectsShortVersion { set; }
        string AffectsFullVersion { set; }
        string FixShortVersion { set; }
        string FixFullVersion { set; }

        string LabelBacklog { set; }
        string LabelNaoPlanejado { set; }
    }
}
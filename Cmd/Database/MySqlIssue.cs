using System;
using System.Linq;
using JiraQueries.Bussiness.Models;

namespace Cmd.Database {
    public class MySqlIssue {
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
        public string SprintRelease { get; set; }
        public string Sprint { get; set; }
        public string Epic { get; set; }
        public int? Subtasks { get; set; }
        public string AssigneeKey { get; set; }
        public string AssigneeName { get; set; }
        public string AssigneeShortName { get; set; }
        public string ReviewerKey { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerShortName { get; set; }
        public int? Storypoints { get; set; }
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
        public string ProductionFactor { get; set; }
        public string TechnologyUpdateFactor { get; set; }
        public string ImplementerFunding { get; set; }
        public string BugCause { get; set; }
        public string BugSource { get; set; }
        public string ServiceDesk { get; set; }
        public string LabelBacklog { get; set; }
        public string LabelNaoPlanejado { get; set; }

        public void Fill(IssueViewModel issueViewModel) {
            Date = issueViewModel.Milestones.ResolvedAt.Date;
            DateYear = issueViewModel.Milestones.ResolvedAt.Year;
            DateMonth = issueViewModel.Milestones.ResolvedAt.Month.Value;
            DateQuarter = issueViewModel.Milestones.ResolvedAt.Quarter.Value;
            DateDayOfWeek = (int) issueViewModel.Milestones.ResolvedAt.Week.DayValue;
            ProjectKey = issueViewModel.Project.Key;
            ProjectName = issueViewModel.Project.Name;
            Key = issueViewModel.Key;
            Title = issueViewModel.Title;
            Name = $"({issueViewModel.Key}) {issueViewModel.Title}";
            Type = issueViewModel.IssueType;
            Status = issueViewModel.Status;
            SprintRelease = issueViewModel.Sprint?.Release;
            Sprint = issueViewModel.Sprint?.Name;
            Epic = issueViewModel.Epic;
            Subtasks = issueViewModel.SubTasks?.Count;
            AssigneeKey = issueViewModel.Assignee?.Key;
            AssigneeName = issueViewModel.Assignee?.Name;
            AssigneeShortName = CreateShortName(issueViewModel.Assignee?.Name);
            ReviewerKey = issueViewModel.Reviewer?.Key;
            ReviewerName = issueViewModel.Reviewer?.Name;
            ReviewerShortName = CreateShortName(issueViewModel.Reviewer?.Name);
            Storypoints = issueViewModel.StoryPoints;
            HoursSpent = Round2Digits(issueViewModel.TimeSpent?.TotalIncludingSubstask?.TotalHours);
            HoursSpentPerAssignee = Round2Digits(issueViewModel.TimeSpent?.Assignee?.TotalHours);
            HoursSpentPerAssigneePerc = Round2Digits(issueViewModel.TimeSpent?.AssigneePercentual);
            HoursSpentPerReviewer = Round2Digits(issueViewModel.TimeSpent?.Reviewer?.TotalHours);
            HoursSpentPerReviewerPerc = Round2Digits(issueViewModel.TimeSpent?.ReviewerPercentual);
            HoursSpentPerOthers = Round2Digits(issueViewModel.TimeSpent?.Others?.TotalHours);
            HoursSpentPerOthersPerc = Round2Digits(issueViewModel.TimeSpent?.OthersPercentual);
            DaysToStart = Round2Digits(issueViewModel.Milestones.TimeToStart?.TotalDays);
            DaysToReject = Round2Digits(issueViewModel.Milestones.TimeToReject?.TotalDays);
            DaysToDone = Round2Digits(issueViewModel.Milestones.TimeToDone?.TotalDays);
            DaysToResolve = Round2Digits(issueViewModel.Milestones.TimeToResolve?.TotalDays);
            DaysInProgress = Round2Digits(issueViewModel.Milestones.TimeInProgress?.TotalDays);
            DaysInPullRequest = Round2Digits(issueViewModel.Milestones.TimeInPullRequest?.TotalDays);
            DaysInTest = Round2Digits(issueViewModel.Milestones.TimeInTest?.TotalDays);
            DaysInImpediment = Round2Digits(issueViewModel.Milestones.TimeInImpediment?.TotalDays);
            DaysInDevelopment = Round2Digits(issueViewModel.Milestones.TimeInDevelopment?.TotalDays);
            ProductionFactor = issueViewModel.CustomFields.FatorProdutizacao;
            TechnologyUpdateFactor = issueViewModel.CustomFields.FatorAtualizacaoTecnologica;
            ImplementerFunding = issueViewModel.CustomFields.FinanciadorImplementacao;
            BugCause = issueViewModel.CustomFields.CausaRaiz;
            BugSource = issueViewModel.CustomFields.Fonte;
            ServiceDesk = CreateServiceDesk();
            LabelBacklog = AdjustLabel(issueViewModel.Labels?.Backlog, "Backlog", "Compromisso");
            LabelNaoPlanejado = AdjustLabel(issueViewModel.Labels?.NaoPlanejado, "NaoPlanejado", "Planejado");

            string AdjustLabel(BoolViewModel value, string labelPositive, string labelNegative)
                => value?.Value == true ? labelPositive : labelNegative;

            string CreateServiceDesk() {
                if (issueViewModel.CustomFields.Chamado is null) {
                    return null;
                }
                if (issueViewModel.CustomFields.Chamado) {
                    return "INC";
                }
                return "No";
            }

            double? Round2Digits(double? value) {
                if (value is null) {
                    return null;
                }
                return Math.Round(value.Value, 2);
            }

            string CreateShortName(string name) {
                if (name is null) {
                    return null;
                }

                var parts = name.Split(' ', '.', '.');
                switch (parts.Length) {
                    case 0:
                    case 1:
                        return name;
                    default:
                        return parts.First();
                }
            }
        }
    }
}
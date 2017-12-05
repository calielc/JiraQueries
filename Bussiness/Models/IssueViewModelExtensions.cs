using System;
using System.Linq;

namespace JiraQueries.Bussiness.Models {
    public static class IssueViewModelExtensions {
        public static void CopyTo(this IssueViewModel issueViewModel, ISimpleIssueSettable settable) {
            settable.Date = issueViewModel.Milestones.ResolvedAt.Date;
            settable.DateYear = issueViewModel.Milestones.ResolvedAt.Year;
            settable.DateMonth = issueViewModel.Milestones.ResolvedAt.Month.Value;
            settable.DateQuarter = issueViewModel.Milestones.ResolvedAt.Quarter.Value;
            settable.DateDayOfWeek = (int) issueViewModel.Milestones.ResolvedAt.Week.DayValue;

            settable.ProjectKey = issueViewModel.Project.Key;
            settable.ProjectName = issueViewModel.Project.Name;

            settable.Key = issueViewModel.Key;
            settable.Title = issueViewModel.Title;
            settable.Name = $"({issueViewModel.Key}) {issueViewModel.Title}";

            settable.Type = issueViewModel.IssueType;
            settable.Status = issueViewModel.Status;
            settable.Epic = issueViewModel.Epic;
            settable.Subtasks = issueViewModel.SubTasks?.Count;
            settable.Storypoints = issueViewModel.StoryPoints;

            settable.SprintRelease = issueViewModel.Sprint?.Release;
            settable.Sprint = issueViewModel.Sprint?.Name;

            settable.AssigneeKey = issueViewModel.Assignee?.Key;
            settable.AssigneeName = issueViewModel.Assignee?.Name;
            settable.AssigneeShortName = CreateShortName(issueViewModel.Assignee?.Name);

            settable.ReviewerKey = issueViewModel.Reviewer?.Key;
            settable.ReviewerName = issueViewModel.Reviewer?.Name;
            settable.ReviewerShortName = CreateShortName(issueViewModel.Reviewer?.Name);

            settable.HoursSpent = Round2Digits(issueViewModel.TimeSpent?.TotalIncludingSubstask?.TotalHours);
            settable.HoursSpentPerAssignee = Round2Digits(issueViewModel.TimeSpent?.Assignee?.TotalHours);
            settable.HoursSpentPerAssigneePerc = Round2Digits(issueViewModel.TimeSpent?.AssigneePercentual);
            settable.HoursSpentPerReviewer = Round2Digits(issueViewModel.TimeSpent?.Reviewer?.TotalHours);
            settable.HoursSpentPerReviewerPerc = Round2Digits(issueViewModel.TimeSpent?.ReviewerPercentual);
            settable.HoursSpentPerOthers = Round2Digits(issueViewModel.TimeSpent?.Others?.TotalHours);
            settable.HoursSpentPerOthersPerc = Round2Digits(issueViewModel.TimeSpent?.OthersPercentual);

            settable.DaysToStart = Round2Digits(issueViewModel.Milestones.TimeToStart?.TotalDays);
            settable.DaysToReject = Round2Digits(issueViewModel.Milestones.TimeToReject?.TotalDays);
            settable.DaysToDone = Round2Digits(issueViewModel.Milestones.TimeToDone?.TotalDays);
            settable.DaysToResolve = Round2Digits(issueViewModel.Milestones.TimeToResolve?.TotalDays);

            settable.DaysInProgress = Round2Digits(issueViewModel.Milestones.TimeInProgress?.TotalDays);
            settable.DaysInPullRequest = Round2Digits(issueViewModel.Milestones.TimeInPullRequest?.TotalDays);
            settable.DaysInTest = Round2Digits(issueViewModel.Milestones.TimeInTest?.TotalDays);
            settable.DaysInImpediment = Round2Digits(issueViewModel.Milestones.TimeInImpediment?.TotalDays);
            settable.DaysInDevelopment = Round2Digits(issueViewModel.Milestones.TimeInDevelopment?.TotalDays);

            settable.Productization = issueViewModel.CustomFields.Produtizacao;
            settable.ProductizationFactor = issueViewModel.CustomFields.ProdutizacaoPercentual;
            settable.ProductizationHours = Round2Digits(issueViewModel.CustomFields.ProdutizacaoPercentual * issueViewModel.TimeSpent?.TotalIncludingSubstask?.TotalHours);

            settable.TechnologyUpdate = issueViewModel.CustomFields.AtualizacaoTecnologica;
            settable.TechnologyUpdateFactor = issueViewModel.CustomFields.AtualizacaoTecnologicaPercentual;
            settable.TechnologyUpdateHours = Round2Digits(issueViewModel.CustomFields.AtualizacaoTecnologicaPercentual * issueViewModel.TimeSpent?.TotalIncludingSubstask?.TotalHours);

            settable.ImplementerFunding = issueViewModel.CustomFields.FinanciadorImplementacao;

            settable.BugCause = issueViewModel.CustomFields.CausaRaiz;
            settable.BugSource = issueViewModel.CustomFields.Fonte;
            settable.ServiceDesk = CreateServiceDesk(issueViewModel);

            settable.LabelBacklog = AdjustLabel(issueViewModel.Labels?.Backlog, "Backlog", "Compromisso");
            settable.LabelNaoPlanejado = AdjustLabel(issueViewModel.Labels?.NaoPlanejado, "NaoPlanejado", "Planejado");
        }

        private static string CreateShortName(string name) {
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

        private static double? Round2Digits(double? value) {
            if (value is null) {
                return null;
            }
            return Math.Round(value.Value, 2);
        }

        private static string AdjustLabel(BoolViewModel value, string labelPositive, string labelNegative)
            => value?.Value == true ? labelPositive : labelNegative;

        private static string CreateServiceDesk(IssueViewModel viewModel) {
            if (viewModel.CustomFields.Chamado is null) {
                return null;
            }
            if (viewModel.CustomFields.Chamado) {
                return "INC";
            }
            return "No";
        }
    }
}
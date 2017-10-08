using JiraQueries.Bussiness.Export;
using JiraQueries.Bussiness.Models;

namespace JiraQueries.Bussiness.Services {
    public static class DefaultMappingBuilder {
        public static MappingBuilder<IssueViewModel> ForResolvedIssues => new MappingBuilder<IssueViewModel>()
            .Add("date", item => item.Milestones.ResolvedAt.Date, "yyyy-MM-dd")
            .Add("dateWeekDay", item => {
                var dayOfWeek = item.Milestones.ResolvedAt.Week.DayValue;
                return $"{(int) dayOfWeek}-{dayOfWeek.ToString().Substring(0, 3)}";
            })
            .Add("dateFortnight", item => item.Milestones.ResolvedAt.Fortnight.Value)
            .Add("dateMonth", item => item.Milestones.ResolvedAt.Month.Value)
            .Add("dateQuarter", item => item.Milestones.ResolvedAt.Quarter.Value)
            .Add("dateBimester", item => item.Milestones.ResolvedAt.Bimester.Value)
            .Add("dateSemester", item => item.Milestones.ResolvedAt.Semester.Value)
            .Add("dateWeekYear", item => item.Milestones.ResolvedAt.Week.YearValue)
            .Add("dateYear", item => item.Milestones.ResolvedAt.Year)

            .Add("key", item => item.Key)
            .Add("title", item => item.Summary)
            .Add("type", item => item.IssueType)

            .Add("release", item => item.Sprint?.Release)
            .Add("sprint", item => item.Sprint?.Name)

            .Add("epic", item => item.Epic)
            .Add("subtasks", item => item.SubTasks?.Count)

            .Add("storypoints", item => item.StoryPoints)
            .Add("hoursSpent", item => item.AggregateTimespent?.TotalHours, "0.00")

            .Add("productionFactor", item => item.CustomFields.FatorProdutizacao)
            .Add("technologyUpdateFactor", item => item.CustomFields.FatorAtualizacaoTecnologica)
            .Add("implementerFunding", item => item.CustomFields.FinanciadorImplementacao)
            .Add("bugCause", item => item.CustomFields.CausaRaiz)
            .Add("bugSource", item => item.CustomFields.Fonte)
            .Add("serviceDesk", item => item.CustomFields.Chamado?.Text);
    }
}
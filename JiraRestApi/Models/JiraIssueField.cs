using System;
using Newtonsoft.Json;

namespace JiraQueries.JiraRestApi.Models {
    public class JiraIssueField {
        public JiraCommon IssueType { get; set; }

        public long? Timespent { get; set; }

        public long? AggregateTimespent { get; set; }

        public string[] Labels { get; set; }

        public JiraCommon Project { get; set; }

        public JiraCommon Assignee { get; set; }

        public JiraCommon Status { get; set; }

        public string Summary { get; set; }

        public JiraCommon Creator { get; set; }

        public JiraCommon Reporter { get; set; }

        [JsonProperty("customfield_10008")]
        public string Epic { get; set; }

        public JiraCommon[] SubTasks { get; set; }

        [JsonProperty("customfield_10400")]
        public JiraCommon CausaRaiz { get; set; }

        [JsonProperty("customfield_10200")]
        public JiraCommon Reviewer { get; set; }

        [JsonProperty("customfield_11000")]
        public JiraCommon FinanciadorImplementacao { get; set; }

        [JsonProperty("customfield_11002")]
        public JiraCommon FatorProdutizacao { get; set; }

        [JsonProperty("customfield_11004")]
        public JiraCommon FatorAtualizacaoTecnologica { get; set; }

        [JsonProperty("customfield_11005")]
        public JiraCommon Fonte { get; set; }

        [JsonProperty("customfield_12200")]
        public JiraCommon[] Ofertas { get; set; }

        [JsonProperty("customfield_11001")]
        public JiraCommon[] Contratos { get; set; }

        [JsonProperty("customfield_10300")]
        public JiraCommon[] Clientes { get; set; }

        [JsonProperty("customfield_10005")]
        public double? StoryPoints { get; set; }

        [JsonProperty("customfield_10007")]
        public string[] Sprints { get; set; }

        public JiraCommon[] FixVersions { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public DateTime? LastViewed { get; set; }
    }
}
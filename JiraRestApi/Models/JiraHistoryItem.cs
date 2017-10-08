using Newtonsoft.Json;

namespace JiraQueries.JiraRestApi.Models {
    public class JiraHistoryItem {
        public string Field { get; set; }

        [JsonProperty("From")]
        public string FromValue { get; set; }

        [JsonProperty("FromString")]
        public string FromText { get; set; }

        [JsonProperty("To")]
        public string ToValue { get; set; }

        [JsonProperty("ToString")]
        public string ToText { get; set; }
    }
}
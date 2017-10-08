using System;

namespace JiraQueries.JiraRestApi.Models
{
    public class JiraHistory {
        public JiraCommon Author { get; set; }
        public DateTime Created { get; set; }
        public JiraHistoryItem[] Items { get; set; }
    }
}
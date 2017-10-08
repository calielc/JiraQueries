namespace JiraQueries.JiraRestApi.Models {
    public class JiraIssueWrapper {
        public int StartAt { get; set; }

        public int MaxResults { get; set; }

        public int Total { get; set; }

        public JiraIssue[] Issues { get; set; }
    }
}
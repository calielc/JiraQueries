namespace JiraQueries.JiraRestApi.Models {
    public class JiraIssue {
        public string Key { get; set; }

        public JiraIssueField Fields { get; set; }

        public JiraChangeLog ChangeLog { get; set; }
    }
}
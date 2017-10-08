namespace JiraQueries.JiraRestApi.Jql {
    public struct JqlIssueType : IJqlValue {
        private readonly string _value;

        private JqlIssueType(string value) {
            _value = value;
        }

        public static JqlIssueType Bug = new JqlIssueType("Bug");
        public static JqlIssueType Epic = new JqlIssueType("Epic");
        public static JqlIssueType Story = new JqlIssueType("Story");
        public static JqlIssueType Task = new JqlIssueType("Task");
        public static JqlIssueType SubTask = new JqlIssueType("Sub-task");

        public override string ToString() => _value;

        string IUrlResolver.Resolve() => _value;
    }
}
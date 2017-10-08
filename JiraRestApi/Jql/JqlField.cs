namespace JiraQueries.JiraRestApi.Jql {
    public interface IJqlField : IUrlResolver { }

    public struct JqlField<T> : IJqlField where T : IJqlValue {
        private readonly string _value;

        internal JqlField(string value) {
            _value = value;
        }

        public override string ToString() => _value;

        string IUrlResolver.Resolve() => _value;
    }

    public static class JqlField {
        public static JqlField<JqlValueString> Project = new JqlField<JqlValueString>("project");
        public static JqlField<JqlStatus> Status = new JqlField<JqlStatus>("status");
        public static JqlField<JqlIssueType> IssueType = new JqlField<JqlIssueType>("issuetype");
        public static JqlField<JqlValueDateTime> Resolved = new JqlField<JqlValueDateTime>("resolved");
    }
}
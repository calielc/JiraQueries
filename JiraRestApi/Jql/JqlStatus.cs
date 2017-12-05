namespace JiraQueries.JiraRestApi.Jql {
    public struct JqlStatus : IJqlValue {
        private readonly string _value;

        private JqlStatus(string value) {
            _value = value;
        }

        public static JqlStatus ToDo = new JqlStatus("To Do");
        public static JqlStatus Done = new JqlStatus("Done");
        public static JqlStatus Rejected = new JqlStatus("Rejected");

        public override string ToString() => _value;

        string IUrlResolver.Resolve() => _value;
    }
}
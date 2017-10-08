namespace JiraQueries.JiraRestApi.Jql {
    public struct JqlExpand : IUrlResolver {
        private readonly string _value;

        private JqlExpand(string value) {
            _value = value;
        }

        public override string ToString() => _value;

        string IUrlResolver.Resolve() => _value;

        public static JqlExpand ChangeLog = new JqlExpand("changelog");
    }
}
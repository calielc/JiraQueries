namespace JiraQueries.JiraRestApi.Jql {
    public sealed class JqlValueString : JqlValue<string> {
        private JqlValueString(string value) {
            Value = value;
        }

        public string Value { get; }

        public override string ToString() => Value;

        public override string Resolve() => Value;

        public static implicit operator JqlValueString(string value) => new JqlValueString(value);
    }
}
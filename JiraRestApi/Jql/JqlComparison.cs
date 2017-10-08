namespace JiraQueries.JiraRestApi.Jql {
    internal struct JqlComparison : IJql {
        private readonly IJqlField _field;
        private readonly IJqlValue _value;
        private readonly string _sign;

        internal JqlComparison(IJqlField field, IJqlValue value, string sign) {
            _field = field;
            _value = value;
            _sign = sign;
        }

        public string Resolve() => $"{_field.Resolve()} {_sign} {_value.Resolve()}";

        public override string ToString() => Resolve();
    }
}
using System.Collections.Generic;

namespace JiraQueries.JiraRestApi.Jql {
    internal struct JqlIn : IJql {
        private readonly IJqlField _field;
        private readonly ResolverCollection<IJqlValue> _values;

        internal JqlIn(IJqlField field, IEnumerable<IJqlValue> values) {
            _field = field;
            _values = values.Merge(", ");
        }

        public string Resolve() {
            return $"{_field.Resolve()} in ({_values.Resolve()})";
        }

        public override string ToString() => Resolve();
    }
}
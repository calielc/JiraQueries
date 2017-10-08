using System.Collections.Generic;

namespace JiraQueries.JiraRestApi.Jql {
    internal sealed class JqlAnd : IJql {
        private readonly ResolverCollection<IJql> _conditionals;

        public JqlAnd(IEnumerable<IJql> conditionals) => _conditionals = conditionals.Merge(" AND ");

        public string Resolve() => _conditionals.Resolve();

        public override string ToString() => Resolve();
    }
}
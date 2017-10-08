using System.Collections.Generic;

namespace JiraQueries.JiraRestApi.Jql {
    internal struct JqlOr : IJql {
        private readonly ResolverCollection<IJql> _conditionals;

        internal JqlOr(IEnumerable<IJql> conditionals) => _conditionals = conditionals.Merge(" OR ");

        public string Resolve() => _conditionals.Resolve();

        public override string ToString() => Resolve();
    }
}
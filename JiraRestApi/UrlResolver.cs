using System;
using JiraQueries.JiraRestApi.Jql;

namespace JiraQueries.JiraRestApi {
    internal sealed class UrlResolver : IUrlResolver {
        private readonly string _path;

        public UrlResolver(string path, IJql jql) {
            _path = path;
            Jql = jql;
        }

        public IJql Jql { get; }

        public JqlExpand[] Expands { get; set; } = new JqlExpand[0];

        public JqlValueInt StartAt { get; set; } = 0;

        public JqlValueInt MaxResults { get; set; } = 20;

        public string Resolve() {
            var @params = new[]
            {
                new UrlParam("jql", Jql),
                new UrlParam("expand", Expands.Merge(", ")),
                new UrlParam("startAt", StartAt),
                new UrlParam("maxResults", MaxResults),
            }.Resolve("&");

            return $"{_path}?{@params}";
        }

        public static implicit operator Uri(UrlResolver self) => new Uri(self.Resolve(), UriKind.Relative);

        private struct UrlParam : IUrlResolver {
            private readonly string _key;
            private readonly IUrlResolver _value;

            public UrlParam(string key, IUrlResolver value) {
                _key = key;
                _value = value;
            }

            public string Resolve() => $"{_key}={_value.Resolve()}";
        }
    }
}
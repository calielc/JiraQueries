using System;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using JiraQueries.JiraRestApi;
using Newtonsoft.Json;

namespace JiraQueries.Bussiness.Jira {
    public sealed class JiraAccessPoint {
        private static readonly Lazy<JiraAccessPoint> LazyInstance = new Lazy<JiraAccessPoint>(() => new JiraAccessPoint());
        private readonly string _baseDirectory;

        private JiraAccessPoint() {
            _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            Config = ClientConfig.ReadFromResource("JiraQueries.Bussiness.Jira.JiraAccessPoint.json");

            Client = new JiraApiClient(Config);
        }

        public IJiraAPiClientConfig Config { get; }

        public IJiraApiClient Client { get; }

        public CachedJiraSearch CachedSearch => new CachedJiraSearch(Client, _baseDirectory);

        public static JiraAccessPoint Instance => LazyInstance.Value;

        private sealed class ClientConfig : IJiraAPiClientConfig {
            public string Url { get; set; }

            public string Scheme { get; set; }

            public string Token { get; set; }

            public AuthenticationHeaderValue Authorization => new AuthenticationHeaderValue(Scheme, Token);

            public static IJiraAPiClientConfig ReadFromResource(string name) {
                var assembly = typeof(JiraAccessPoint).Assembly;
                var resourceStream = assembly.GetManifestResourceStream(name);

                using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                    var fileContent = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<ClientConfig>(fileContent);
                }
            }
        }
    }
}
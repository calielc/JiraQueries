using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JiraQueries.JiraRestApi.Jql;
using JiraQueries.JiraRestApi.Models;
using Newtonsoft.Json;

namespace JiraQueries.JiraRestApi {
    public sealed class JiraApiClient : IJiraApiClient {
        private readonly HttpClient _client;

        public JiraApiClient(IJiraAPiClientConfig config) {
            _client = new HttpClient {
                BaseAddress = new Uri(config.Url)
            };

            _client.DefaultRequestHeaders.Authorization = config.Authorization;

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IReadOnlyCollection<JiraIssue>> Search(IJql jql, JqlExpand[] expands) {
            var result = new List<JiraIssue>();

            JiraIssueWrapper wrapper;
            do {
                var response = await _client.GetAsync(new UrlResolver("search", jql) {
                    Expands = expands,
                    StartAt = result.Count
                });
                if (!response.IsSuccessStatusCode) {
                    break;
                }

                var text = await response.Content.ReadAsStringAsync();
                wrapper = JsonConvert.DeserializeObject<JiraIssueWrapper>(text);
                if (wrapper.Issues.Length == 0) {
                    break;
                }

                result.AddRange(wrapper.Issues);
            } while (result.Count < wrapper.Total);

            return result;
        }
    }
}
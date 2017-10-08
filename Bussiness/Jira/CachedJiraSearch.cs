using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JiraQueries.JiraRestApi;
using JiraQueries.JiraRestApi.Jql;
using JiraQueries.JiraRestApi.Models;
using Newtonsoft.Json;

namespace JiraQueries.Bussiness.Jira {
    public class CachedJiraSearch {
        public CachedJiraSearch(IJiraApiClient jiraApiClient, string rootDirectory) {
            JiraApiClient = jiraApiClient;
            RootDirectory = rootDirectory;
        }

        public IJiraApiClient JiraApiClient { get; }

        public string RootDirectory { get; }

        public DateTime MinDate { get; set; } = new DateTime(2015, 01, 01);
        public DateTime IsPastDataBefore { get; set; } = DateTime.Today.AddDays(-20).SetDay(1);

        public async Task<IReadOnlyCollection<JiraIssue>> SplitByResolvedField(IJql jql, JqlExpand[] expands) {
            var result = new List<JiraIssue>();

            var queries = BuildDates().Select(date => new {
                date = date.start,
                jql = jql.And(JqlField.Resolved.GreatherOrEqual(date.start)).And(JqlField.Resolved.Less(date.end)),
                pastData = date.start < IsPastDataBefore,
            });

            var directory = BuildJqlDirectory(jql, expands);

            foreach (var query in queries) {
                var issues = await ResolveSearch(directory, query.date.ToString("yyyy-MM"), query.jql, expands, query.pastData);
                result.AddRange(issues);
            }

            return result;
        }

        private async Task<IReadOnlyCollection<JiraIssue>> ResolveSearch(string directory, string yearMonth, IJql jql, JqlExpand[] expands, bool pastData) {
            var fileName = Path.Combine(directory, $"{yearMonth}.json");
            if (!File.Exists(fileName)) {
                return await LoadFromWeb();
            }

            if (pastData) {
                return LoadFromFile();
            }

            var lastwritten = File.GetLastWriteTimeUtc(fileName);
            if (lastwritten < DateTime.UtcNow.AddMinutes(-30)) {
                return await LoadFromWeb();
            }

            return LoadFromFile();

            IReadOnlyCollection<JiraIssue> LoadFromFile() {
                var readContent = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<JiraIssue[]>(readContent);
            }

            async Task<IReadOnlyCollection<JiraIssue>> LoadFromWeb() {
                var result = await JiraApiClient.Search(jql, expands);

                var writeContent = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText(fileName, writeContent);

                return result;
            }
        }

        private IEnumerable<(DateTime start, DateTime end)> BuildDates() {
            var current = MinDate.SetDay(1);
            var final = DateTime.Today.SetDay(1);

            while (current <= final) {
                yield return (current, current.AddMonths(1));

                current = current.AddMonths(1);
            }
        }

        private string BuildJqlDirectory(IUrlResolver jql, IEnumerable<JqlExpand> expands) {
            var jqlText = jql.Resolve()
                .Replace(">", "GT")
                .Replace("<", "LT");

            var expandsText = expands.Resolve(",");

            var directory = $"{jqlText} + {expandsText}";
            while (directory.Contains("__")) {
                directory = directory.Replace("__", "_");
            }

            var path = Path.Combine(RootDirectory, "cache", directory);
            Directory.CreateDirectory(path);

            return path;
        }
    }
}
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cmd.Database;
using JiraQueries.Bussiness.Jira;
using JiraQueries.Bussiness.Models;
using JiraQueries.Bussiness.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Cmd {
    class Program {
        static async Task Main(string[] args) {
            const string project = "PIM";
            var waitTime = TimeSpan.FromHours(6);

            while (true) {
                Console.WriteLine($"Starting at: {DateTime.Now}");

                await Run(project);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Waiting until {DateTime.Now.Add(waitTime)} to run again");
                System.Threading.Thread.Sleep(waitTime);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static async Task Run(string project) {
            var service = new AllTypesAndDoneOrRejectedService(JiraAccessPoint.Instance) {
                Project = project
            };

            Console.WriteLine("Querying on Jira...");
            var viewModels = await service.Raw();

            var pool = GetPool();

            Console.Write("Saving...");
            var count = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try {
                Parallel.ForEach(viewModels, viewModel => {
                    using (var context = pool.Rent()) {
                        try {
                            SaveIssue(context, viewModel);
                        }
                        catch (Exception e) {
                            var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);

                            Console.WriteLine($"\n{viewModel.Key} with error: \"{e.Message}\".\n{serialized}");
                        }
                    }
                    count += 1;
                    if (count % 10 == 0) {
                        Console.Write(".");
                    }
                });
                Console.WriteLine();
            }
            finally {
                stopwatch.Stop();
                Console.WriteLine(
                    $"{count} items in {stopwatch.Elapsed} = {count / stopwatch.Elapsed.TotalSeconds:0.0} items/second");
            }
        }

        private static DbContextPool<MySqlContext> GetPool() {
            var optionsBuilder = new DbContextOptionsBuilder<MySqlContext>();
            optionsBuilder.UseMySqlConfiguration();

            return new DbContextPool<MySqlContext>(optionsBuilder.Options);
        }

        private static void SaveIssue(MySqlContext context, IssueViewModel viewModel) {
            var dbIssue = context.Issues.SingleOrDefault(find => find.Key == viewModel.Key);
            if (dbIssue == null) {
                dbIssue = new MySqlIssue();
                context.Issues.Add(dbIssue);
            }

            dbIssue.Fill(viewModel);

            context.SaveChanges();
        }
    }
}

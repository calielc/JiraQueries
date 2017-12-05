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
        private static readonly TimeSpan WaitTime = TimeSpan.FromHours(3);

        static async Task Main(string[] args) {
            var service = CreateService("PIM");
            while (true) {
                Console.WriteLine($"Starting at: {DateTime.Now}");

                await Run(service);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Waiting until {DateTime.Now.Add(WaitTime)} to run again");
                System.Threading.Thread.Sleep(WaitTime);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static async Task Run(IJiraService service) {
            Console.WriteLine("Querying on Jira...");
            var viewModels = await service.Load();

            var count = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try {
                var pool = GetPool();
                Console.Write("Saving...");

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
                Console.WriteLine($"{count} items in {stopwatch.Elapsed} = {count / stopwatch.Elapsed.TotalSeconds:0.0} items/second");
            }
        }

        private static IJiraService CreateService(string project) {
            return new AllTypesAndDoneOrRejectedService(JiraAccessPoint.Instance) {
                Project = project
            };
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

            viewModel.CopyTo(dbIssue);

            context.SaveChanges();
        }
    }
}

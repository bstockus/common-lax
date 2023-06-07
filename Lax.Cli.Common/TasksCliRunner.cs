using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lax.Cli.Abstractions;

namespace Lax.Cli.Common {

    public class TasksCliRunner : ICliRunner {

        private readonly IEnumerable<CliTask> _cliTasks;

        public string RunnerName => "RUN";

        public TasksCliRunner(IEnumerable<CliTask> cliTasks) => _cliTasks = cliTasks;

        public async Task Run(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("Error: You need to specify a task to run.");
                return;
            }

            var commandArgs = new Stack<string>(args.Reverse());

            while (commandArgs.Any() && commandArgs.Peek().StartsWith(":")) {
                var taskName = commandArgs.Pop().Replace(":", "");

                var taskArgs = new List<Tuple<string, string>>();
                var taskFlags = new List<string>();

                if (commandArgs.Any()) {
                    while (commandArgs.Any() &&
                           (commandArgs.Peek().Contains("=") || commandArgs.Peek().StartsWith("-"))) {
                        var nextArg = commandArgs.Pop();

                        if (nextArg.StartsWith("-")) {
                            taskFlags.Add(nextArg.Replace("-", "").ToUpper().Trim());
                        } else {
                            var fullArgSplit = nextArg.Split('=');
                            if (fullArgSplit.Length != 2) {
                                Console.WriteLine($"Error Processing Argument: '{nextArg}'");
                                return;
                            }

                            taskArgs.Add(new Tuple<string, string>(fullArgSplit[0].ToUpper().Trim(),
                                fullArgSplit[1].Replace("\"", "")));
                        }
                    }
                }

                var cliTask =
                    _cliTasks.FirstOrDefault(_ =>
                        _.Name.ToUpperInvariant().Equals(taskName.ToUpperInvariant()));

                if (cliTask == null) {
                    Console.WriteLine($"Error: no command called '{taskName}' exists to be run.");
                    return;
                }

                await cliTask.Run(taskArgs.ToLookup(_ => _.Item1, _ => _.Item2), taskFlags);
            }
        }

    }

}
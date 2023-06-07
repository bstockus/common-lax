using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lax.Cli.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Lax.Cli.Common {

    public class CliExecutor {

        private readonly IStartup _startup;

        public CliExecutor(IStartup startup) => _startup = startup;

        public async Task Run(string[] args) {
            var argStack = new Stack<string>(args.Reverse());
            var services = _startup.ConfigureServices(new ServiceCollection());

            using (var scope = services.CreateScope()) {
                var cliRunners = scope.ServiceProvider.GetRequiredService<IEnumerable<ICliRunner>>().ToList();
                var cliDecorators = scope.ServiceProvider.GetRequiredService<IEnumerable<ICliDecorator>>().ToList();

                while (argStack.Any() && argStack.Peek().StartsWith("^")) {
                    var decoratorSplit = argStack.Pop().Replace("^", "").Split('=');

                    var decoratorName = decoratorSplit[0];
                    var decoratorValue = decoratorSplit[1];

                    var decorator =
                        cliDecorators.FirstOrDefault(_ => _.DecoratorName.ToUpper().Equals(decoratorName.ToUpper()));

                    if (decorator == null) {
                        Console.WriteLine($"Decorator for {decoratorName} not found!");
                        return;
                    }

                    await decorator.Run(decoratorValue);
                }

                var runnerName = argStack.Pop();

                var runner = cliRunners.FirstOrDefault(_ => _.RunnerName.ToUpper().Equals(runnerName.ToUpper()));

                if (runner == null) {
                    Console.WriteLine($"Runner for {runnerName} not found!");
                    return;
                }

                await runner.Run(argStack.ToArray());
            }
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Lax.Business.Bus.Cli.TypeParsers;
using Lax.Cli.Abstractions;
using Lax.Serialization.Yaml;
using MediatR;

namespace Lax.Business.Bus.Cli {

    public class BusCliRunner : ICliRunner {

        private readonly IMediator _mediator;
        private readonly IYamlSerializer _yamlSerializer;
        private readonly BusRequestTypesProvider _busRequestTypesProvider;
        private readonly BusCliTypeParserProvider _busCliTypeParserProvider;

        public string RunnerName => "BUS";

        public BusCliRunner(IMediator mediator,
            IYamlSerializer yamlSerializer,
            BusRequestTypesProvider busRequestTypesProvider,
            BusCliTypeParserProvider busCliTypeParserProvider) {
            _mediator = mediator;
            _yamlSerializer = yamlSerializer;
            _busRequestTypesProvider = busRequestTypesProvider;
            _busCliTypeParserProvider = busCliTypeParserProvider;
        }

        public async Task Run(string[] args) =>
            await Task.Run(() => {
                var commandArgs = new Stack<string>(args.Reverse());

                var commandName = commandArgs.Pop();

                var (commandParams, commandFlags) = ParseCommandParams(commandArgs);

                var commandParamsLookup = commandParams.ToLookup(_ => _.Item1, _ => _.Item2);

                var commandType = _busRequestTypesProvider.GetTypeForName(commandName);

                var commandInstance = CreateCommandInstance(commandType, commandFlags, commandParamsLookup);

                ExecuteCommand(commandType, commandInstance);
            });

        private void ExecuteCommand(Type commandType, object commandInstance) {
            if (commandType.GetTypeInfo().ImplementedInterfaces.Any(_ => _ == typeof(IRequest))) {
                var runnerType = typeof(BusRequestRunner<>).GetTypeInfo().MakeGenericType(commandType);

                var runnerObject = Activator.CreateInstance(runnerType);

                runnerType.GetMethods().First(_ => _.Name.Equals("Run")).Invoke(runnerObject, new[] {
                    commandInstance,
                    _mediator
                });
            } else if (commandType.GetTypeInfo().IsClosedTypeOf(typeof(IRequest<>))) {
                var responseInterfaceType = commandType.GetTypeInfo().ImplementedInterfaces
                    .FirstOrDefault(_ => _.IsClosedTypeOf(typeof(IRequest<>)));

                var responseType = responseInterfaceType?.GenericTypeArguments[0];

                var runnerType = typeof(BusRequestRunner<,>).GetTypeInfo().MakeGenericType(commandType, responseType);

                var runnerObject = Activator.CreateInstance(runnerType);

                runnerType.GetMethods().First(_ => _.Name.Equals("Run")).Invoke(runnerObject, new[] {
                    commandInstance,
                    _mediator,
                    _yamlSerializer
                });
            }
        }

        private object CreateCommandInstance(Type commandType, List<string> commandFlags,
            ILookup<string, string> commandParamsLookup) {
            object commandInstance;

            if (commandType.GetConstructors().Any(_ => _.GetParameters().Any())) {
                var constructorInfo = commandType.GetConstructors().First();

                var parametersToPass = new List<object>();

                foreach (var parameterInfo in constructorInfo.GetParameters()) {
                    var paramName = parameterInfo.Name;
                    var paramType = parameterInfo.ParameterType;

                    if (paramType == typeof(bool)) {
                        parametersToPass.Add(commandFlags.Contains(paramName));
                    } else {
                        if (!commandParamsLookup.Contains(paramName)) {
                            Console.WriteLine($"No value provided for property {paramName}");
                            throw new Exception();
                        }

                        parametersToPass.Add(_busCliTypeParserProvider.Parse(paramType,
                            commandParamsLookup[paramName]));
                    }
                }


                commandInstance = Activator.CreateInstance(commandType, parametersToPass.ToArray());
            } else {
                commandInstance = Activator.CreateInstance(commandType);
            }

            foreach (var propertyInfo in commandType.GetProperties().Where(_ => _.CanWrite)) {
                var propertyName = propertyInfo.Name;

                if (propertyInfo.PropertyType == typeof(bool)) {
                    propertyInfo.SetValue(commandInstance, commandFlags.Contains(propertyName));
                } else {
                    if (!commandParamsLookup.Contains(propertyName)) {
                        Console.WriteLine($"No value provided for property {propertyName}");
                        throw new Exception();
                    }

                    propertyInfo.SetValue(commandInstance,
                        _busCliTypeParserProvider.Parse(propertyInfo.PropertyType, commandParamsLookup[propertyName]));
                }
            }

            return commandInstance;
        }

        private (List<Tuple<string, string>> commandParams, List<string> commandFlags) ParseCommandParams(
            Stack<string> commandArgs) {
            var commandParams = new List<Tuple<string, string>>();
            var commandFlags = new List<string>();

            if (!commandArgs.Any()) {
                return (commandParams, commandFlags);
            }

            while (commandArgs.Any() && (commandArgs.Peek().Contains("=") || commandArgs.Peek().StartsWith("-"))) {
                var nextArg = commandArgs.Pop();

                if (nextArg.StartsWith("-")) {
                    commandFlags.Add(nextArg.Replace("-", "").Trim());
                } else {
                    var fullArgSplit = nextArg.Split('=');
                    if (fullArgSplit.Length != 2) {
                        Console.WriteLine($"Error Processing Argument: '{nextArg}'");
                        throw new Exception();
                    }

                    commandParams.Add(new Tuple<string, string>(fullArgSplit[0].Trim(),
                        fullArgSplit[1].Replace("\"", "")));
                }
            }

            return (commandParams, commandFlags);
        }

    }

}
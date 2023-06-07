using System.Reflection;
using Autofac;
using Lax.Cli.Abstractions;

namespace Lax.Cli.Common {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterTasksCli(this ContainerBuilder builder,
            params Assembly[] assemblies) {
            builder.RegisterType<TasksCliRunner>().As<ICliRunner>().SingleInstance();

            builder.RegisterAssemblyTypes(assemblies).AssignableTo<CliTask>().As<CliTask>().InstancePerDependency();

            return builder;
        }

    }

}
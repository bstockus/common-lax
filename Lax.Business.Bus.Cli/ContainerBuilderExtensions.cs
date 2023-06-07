using System.Linq;
using System.Reflection;
using Autofac;
using Lax.Business.Bus.Cli.TypeParsers;
using Lax.Cli.Abstractions;
using MediatR;

namespace Lax.Business.Bus.Cli {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterBusCli(this ContainerBuilder builder, params Assembly[] assemblies) {
            builder.RegisterType<BusCliRunner>().As<ICliRunner>().InstancePerDependency();

            var requestTypes = assemblies.Select(_ => _.GetTypes()).SelectMany(_ => _)
                .Where(_ => _.IsAssignableTo<IBaseRequest>() && !_.IsAbstract && _.IsClass);

            builder.Register(_ => new BusRequestTypesProvider(requestTypes)).AsSelf().SingleInstance();

            builder.RegisterType<BusCliTypeParserProvider>().AsSelf().SingleInstance();
            builder.RegisterBusCliTypeParsers(typeof(ContainerBuilderExtensions).Assembly);

            return builder;
        }

        public static ContainerBuilder RegisterBusCliTypeParsers(this ContainerBuilder builder,
            params Assembly[] assemblies) {
            builder.RegisterAssemblyTypes(assemblies).AssignableTo<IBusCliTypeParser>()
                .As<IBusCliTypeParser>().SingleInstance();

            builder.RegisterAssemblyTypes(assemblies).AssignableTo<ISimpleBusCliTypeParser>()
                .As<ISimpleBusCliTypeParser>().SingleInstance();

            return builder;
        }

    }

}
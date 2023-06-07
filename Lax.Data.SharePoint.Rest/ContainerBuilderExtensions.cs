using System.Linq;
using System.Reflection;
using Autofac;

namespace Lax.Data.SharePoint.Rest {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterSharePointContentTypeProviders(
            this ContainerBuilder builder,
            params Assembly[] assemblies) {

            var entityContentTypeMaps = assemblies
                .SelectMany(_ => _.GetTypes())
                .Where(_ => _.IsClosedTypeOf(typeof(EntityContentTypeMap<>)))
                .ToDictionary(
                    _ => _.BaseType.GetTypeInfo().GenericTypeArguments[0],
                    _ => _);

            builder
                .Register(_ => new SharePointEntityContentTypeProvider(entityContentTypeMaps))
                .As<ISharePointEntityContentTypeProvider>()
                .SingleInstance();

            builder
                .RegisterGeneric(typeof(SharePointDataSource<>))
                .As(typeof(ISharePointDataSource<>))
                .InstancePerDependency();

            return builder;

        }

    }

}
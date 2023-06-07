using Autofac;
using Lax.Business.Authorization.Abstractions;
using Lax.Cli.Abstractions;

namespace Lax.Business.Authorization.Static {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterStaticAuthorizationUserProvider(this ContainerBuilder builder,
            string windowsSid = null) {
            builder.Register(context => new StaticAuthorizationUserProvider(windowsSid))
                .As<IAuthorizationUserProvider>()
                .AsSelf().SingleInstance();
            builder.RegisterType<StaticAuthorizationCliDecorator>().As<ICliDecorator>().InstancePerDependency();

            return builder;
        }

    }

}
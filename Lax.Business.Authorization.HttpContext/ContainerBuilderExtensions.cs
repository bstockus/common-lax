using Autofac;
using Lax.Business.Authorization.Abstractions;

namespace Lax.Business.Authorization.HttpContext {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterHttpContextAuthorizationUserProvider(this ContainerBuilder builder) {
            builder.RegisterType<HttpContextAuthorizationUserProvider>().As<IAuthorizationUserProvider>()
                .InstancePerDependency();

            return builder;
        }

    }

}
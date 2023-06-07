using Autofac;
using Lax.AutoFac.MediatR;

namespace Lax.Business.Bus.Authorization {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterBusAuthorization(this ContainerBuilder builder) {
            builder.RegisterBusBehavior(typeof(BusAuthorizationBehavior<,>));
            return builder;
        }

    }

}
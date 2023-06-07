using Autofac;
using Lax.AutoFac.MediatR;

namespace Lax.Business.Bus.Logging {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterBusLogging(this ContainerBuilder builder) {
            builder.RegisterBusBehavior(typeof(BusLoggingBehavior<,>));

            return builder;
        }

    }

}
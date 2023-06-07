using Autofac;
using Lax.AutoFac.MediatR;

namespace Lax.Business.Bus.Validation {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterBusValidation(this ContainerBuilder builder) {
            builder.RegisterBusBehavior(typeof(BusValidationBehavior<,>));

            return builder;
        }

    }

}
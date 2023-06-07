using Autofac;
using Lax.AutoFac.MediatR;

namespace Lax.Business.Bus.UnitOfWork {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterBusUnitOfWork(this ContainerBuilder builder) {
            builder.RegisterBusBehavior(typeof(BusUnitOfWorkBehavior<,>));

            return builder;
        }

    }

}
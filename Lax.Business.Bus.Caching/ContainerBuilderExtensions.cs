using Autofac;
using Lax.AutoFac.MediatR;

namespace Lax.Business.Bus.Caching {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterBusCaching(this ContainerBuilder builder) {

            builder.RegisterBusBehavior(typeof(BusCachingBehavior<,>));

            return builder;
        }

    }

}
using Autofac;
using NodaTime;

namespace Lax.AutoFac.NodaTime {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterSystemClock(this ContainerBuilder builder) {
            builder.Register(_ => SystemClock.Instance).As<IClock>().InstancePerDependency();

            return builder;
        }

    }

}
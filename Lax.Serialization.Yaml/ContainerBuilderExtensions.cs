using Autofac;
using YamlDotNet.Serialization;

namespace Lax.Serialization.Yaml {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterYamlSerialization(this ContainerBuilder builder) {
            builder.RegisterType<StandardYamlSerializer>().As<IYamlSerializer>().SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterNodaTimeYamlSerialization(this ContainerBuilder builder) {
            builder.RegisterType<InstantConverter>().As<IYamlTypeConverter>().SingleInstance();
            builder.RegisterType<LocalDateConverter>().As<IYamlTypeConverter>().SingleInstance();
            builder.RegisterType<LocalDateTimeConverter>().As<IYamlTypeConverter>().SingleInstance();

            return builder;
        }

    }

}
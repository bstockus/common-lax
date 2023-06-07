using Autofac;

namespace Lax.Serialization.Json {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterJsonNetSerialization(this ContainerBuilder builder) {
            builder.RegisterType<DefaultJsonNetSerializerSettingsProvider>().AsSelf()
                .As<IJsonNetSerializerSettingsProvider>()
                .SingleInstance();
            builder.RegisterType<JsonNetSerializer<DefaultJsonNetSerializerSettingsProvider>>().As<IJsonSerializier>()
                .InstancePerDependency();
            builder.RegisterGeneric(typeof(JsonNetSerializer<>)).As(typeof(IJsonNetSerializer<>))
                .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder
            RegisterJsonNetSerializationSettingsProvider<TSettingsProvider>(this ContainerBuilder builder)
            where TSettingsProvider : IJsonNetSerializerSettingsProvider {
            builder.RegisterType<TSettingsProvider>().AsSelf().As<IJsonNetSerializerSettingsProvider>()
                .SingleInstance();

            return builder;
        }

        public static ContainerBuilder
            RegisterDefaultJsonNetSerializerSettingsConfiguration<TSettingsConfiguration>(this ContainerBuilder builder)
            where TSettingsConfiguration : IJsonNetSerializerSettingsConfiguration {
            builder.RegisterType<TSettingsConfiguration>().As<IJsonNetSerializerSettingsConfiguration>()
                .SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterNodaTimeJsonNetSerialization(this ContainerBuilder builder) {
            builder
                .RegisterDefaultJsonNetSerializerSettingsConfiguration<NodaTimeJsonNetSerializationSettingsConfiguration
                >();

            return builder;
        }

    }

}
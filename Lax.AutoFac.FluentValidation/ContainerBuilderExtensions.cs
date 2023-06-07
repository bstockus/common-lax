using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;

namespace Lax.AutoFac.FluentValidation {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterValidator<TValidatorType, TValidator>(this ContainerBuilder builder) {
            builder.RegisterType<TValidator>().As<AbstractValidator<TValidatorType>>().As<IValidator<TValidatorType>>()
                .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder RegisterValidators(this ContainerBuilder builder, params Assembly[] assemblies) {
            builder.RegisterTypes(assemblies.Select(_ => _.GetTypes()).SelectMany(_ => _).ToArray()).Where(_ =>
                    _.GetTypeInfo().IsClosedTypeOf(typeof(AbstractValidator<>))).AsImplementedInterfaces()
                .InstancePerDependency();

            return builder;
        }

    }

}
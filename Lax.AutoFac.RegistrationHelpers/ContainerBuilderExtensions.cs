using System;
using System.Linq;
using System.Reflection;
using Autofac;

namespace Lax.AutoFac.RegistrationHelpers {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterTypeAsConcreteOfGenericPerDependency(
            this ContainerBuilder builder,
            Type type,
            Type genericType) {
            var concreteType = type.GetTypeInfo()
                .ImplementedInterfaces.FirstOrDefault(ii => ii.GetTypeInfo().IsGenericType &&
                                                            ii.IsClosedTypeOf(genericType));

            if (concreteType == null) {
                throw new InvalidOperationException($"{type.Name} doesn't implement {genericType.Name}");
            }

            builder.RegisterType(type).As(concreteType).InstancePerDependency();

            return builder;
        }

    }

}
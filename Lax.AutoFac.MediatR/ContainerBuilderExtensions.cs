using System;
using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;

namespace Lax.AutoFac.MediatR {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterMediatRHandlers(this ContainerBuilder builder,
            params Assembly[] assemblies) {
            var mediatrOpenTypes = new[] {
                typeof(IRequestHandler<,>),
                typeof(IRequestHandler<>),
                typeof(INotificationHandler<>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes) {
                builder
                    .RegisterAssemblyTypes(assemblies)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }


            return builder;
        }

        public static ContainerBuilder RegisterMediatRBus(this ContainerBuilder builder) {
            //builder
            //    .RegisterSource(new ContravariantRegistrationSource());

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();


            // It appears Autofac returns the last registered types first
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx => {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            return builder;
        }

        public static ContainerBuilder
            RegisterBusPreProcessorBehavior(this ContainerBuilder builder, Type requestPreProcessorType) {
            builder.RegisterGeneric(requestPreProcessorType).As(typeof(IRequestPreProcessor<>))
                .InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder
            RegisterBusPostProcessorBehavior(this ContainerBuilder builder, Type requestPostProcessorType) {
            builder.RegisterGeneric(requestPostProcessorType).As(typeof(IRequestPostProcessor<,>))
                .InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder RegisterBusBehavior(this ContainerBuilder builder, Type behaviorType) {
            builder.RegisterGeneric(behaviorType).As(typeof(IPipelineBehavior<,>)).InstancePerDependency();

            return builder;
        }

    }

}
using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;

namespace Lax.Data.Entities.EntityFrameworkCore {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterUnitOfWork(this ContainerBuilder builder) {
            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>))
                .InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder RegisterEntityFrameworkModelBuilders(
            this ContainerBuilder builder,
            params Assembly[] assemblies) {
            builder.RegisterAssemblyTypes(assemblies).AssignableTo<IEntityFrameworkModelBuilder>()
                .AsImplementedInterfaces().InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder RegisterEntityFrameworkDbSetProviders(
            this ContainerBuilder builder,
            params Assembly[] assemblies) {
            var modelBuilders = assemblies.Select(_ => _.GetTypes()).SelectMany(_ => _)
                .Where(_ => _.IsAssignableTo<IEntityFrameworkModelBuilder>()).ToList();

            foreach (var modelBuilderInstance in modelBuilders.Select(modelBuilder =>
                Activator.CreateInstance(modelBuilder) as IEntityFrameworkModelBuilder)) {
                builder.Register(_ => {
                    var unitOfWork =
                        _.Resolve(typeof(IUnitOfWork<>).MakeGenericType(
                            modelBuilderInstance?.DbContextType)) as IUnitOfWork;

                    var getDbSetMethod = typeof(IUnitOfWork).GetTypeInfo().GetMethod("GetDbSet")
                        ?.MakeGenericMethod(modelBuilderInstance?.EntityType);

                    var dbSet = getDbSetMethod?.Invoke(unitOfWork, null);

                    return dbSet;
                }).As(typeof(DbSet<>).MakeGenericType(modelBuilderInstance?.EntityType)).InstancePerDependency();
            }

            return builder;
        }

        public static ContainerBuilder RegisterEntityFrameworkContext<TContext>(this ContainerBuilder builder)
            where TContext : DbContext {
            builder.RegisterType<TContext>().AsSelf().InstancePerDependency();

            return builder;
        }

    }

}
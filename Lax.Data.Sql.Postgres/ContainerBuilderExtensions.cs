using System;
using Autofac;

namespace Lax.Data.Sql.Postgres {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterPostgresConnectionProvider<TPostgresSqlConnection>(
            this ContainerBuilder builder, string connectionString)
            where TPostgresSqlConnection : PostgresSqlConnection =>
            builder.RegisterPostgresConnectionProvider<TPostgresSqlConnection>(connectionString,
                TimeSpan.FromSeconds(30));

        public static ContainerBuilder RegisterPostgresConnectionProvider<TPostgresSqlConnection>(
            this ContainerBuilder builder, string connectionString, TimeSpan timeOutTimeSpan)
            where TPostgresSqlConnection : PostgresSqlConnection {
            builder.Register(_ =>
                    new PostgresConnectionProvider<TPostgresSqlConnection>(connectionString, timeOutTimeSpan))
                .As<PostgresConnectionProvider<TPostgresSqlConnection>>()
                .As<IPostgresConnectionProvider<TPostgresSqlConnection>>()
                .As<ISqlConnectionProvider<TPostgresSqlConnection>>().InstancePerLifetimeScope();

            return builder;
        }

    }

}
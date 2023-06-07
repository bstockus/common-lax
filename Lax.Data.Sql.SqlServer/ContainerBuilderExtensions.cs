using System;
using Autofac;

namespace Lax.Data.Sql.SqlServer {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder
            RegisterSqlServerConnectionProvider<TSqlServerConnection>(this ContainerBuilder builder,
                string connectionString)
            where TSqlServerConnection : SqlServerConnection =>
            builder.RegisterSqlServerConnectionProvider<TSqlServerConnection>(connectionString,
                TimeSpan.FromSeconds(30));

        public static ContainerBuilder
            RegisterSqlServerConnectionProvider<TSqlServerConnection>(this ContainerBuilder builder,
                string connectionString, TimeSpan timeOutTimeSpan)
            where TSqlServerConnection : SqlServerConnection {
            builder.Register(_ =>
                    new SqlServerConnectionProvider<TSqlServerConnection>(connectionString, timeOutTimeSpan))
                .As<SqlServerConnectionProvider<TSqlServerConnection>>()
                .As<ISqlServerConnectionProvider<TSqlServerConnection>>()
                .As<ISqlConnectionProvider<TSqlServerConnection>>()
                .InstancePerLifetimeScope();

            return builder;
        }

    }

}
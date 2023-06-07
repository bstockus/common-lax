using System;
using Autofac;

namespace Lax.Data.Sql.Oracle {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterOracleConnectionProvider<TOracleSqlConnection>(
            this ContainerBuilder builder, string connectionString) where TOracleSqlConnection : OracleSqlConnection =>
            builder.RegisterOracleConnectionProvider<TOracleSqlConnection>(connectionString, TimeSpan.FromSeconds(30));

        public static ContainerBuilder RegisterOracleConnectionProvider<TOracleSqlConnection>(
            this ContainerBuilder builder, string connectionString, TimeSpan timeOutTimeSpan)
            where TOracleSqlConnection : OracleSqlConnection {
            builder.Register(_ => new OracleConnectionProvider<TOracleSqlConnection>(connectionString, timeOutTimeSpan))
                .As<OracleConnectionProvider<TOracleSqlConnection>>()
                .As<IOracleConnectionProvider<TOracleSqlConnection>>()
                .As<ISqlConnectionProvider<TOracleSqlConnection>>().InstancePerLifetimeScope();

            return builder;
        }

    }

}
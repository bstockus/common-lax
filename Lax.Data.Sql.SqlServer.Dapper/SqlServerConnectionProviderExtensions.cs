using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Lax.Data.Sql.SqlServer.Dapper {

    public static class SqlServerConnectionProviderExtensions {

        public static async Task<IEnumerable<TResult>> QueryAsync<TSqlConnection, TResult>(
            this ISqlServerConnectionProvider<TSqlConnection> sqlConnectionProvider, string commandText,
            object parameters = null, CancellationToken cancellationToken = default)
            where TSqlConnection : SqlServerConnection {
            await using var sqlConnection = await sqlConnectionProvider.GetSqlServerConnectionAsync(cancellationToken);
            return await sqlConnection.QueryAsync<TResult>(new CommandDefinition(
                commandText,
                parameters,
                cancellationToken: cancellationToken));
        }

    }

}
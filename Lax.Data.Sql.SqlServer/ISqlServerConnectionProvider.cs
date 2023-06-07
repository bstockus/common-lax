using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Lax.Data.Sql.SqlServer {

    public interface ISqlServerConnectionProvider<TSqlServerConnection> : ISqlConnectionProvider<TSqlServerConnection>
        where TSqlServerConnection : SqlServerConnection {

        Task<SqlConnection> GetSqlServerConnectionAsync(
            CancellationToken cancellationToken = default);

    }

}
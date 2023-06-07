using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace Lax.Data.Sql.Postgres {

    public interface
        IPostgresConnectionProvider<TPostgresSqlConnection> : ISqlConnectionProvider<TPostgresSqlConnection>
        where TPostgresSqlConnection : PostgresSqlConnection {

        Task<NpgsqlConnection> GetNpgsqlConnectionAsync(
            CancellationToken cancellationToken = default);

    }

}
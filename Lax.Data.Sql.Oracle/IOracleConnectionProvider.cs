using System.Threading;
using System.Threading.Tasks;
using Devart.Data.Oracle;

namespace Lax.Data.Sql.Oracle {

    public interface IOracleConnectionProvider<TOracleConnection> : ISqlConnectionProvider<TOracleConnection>
        where TOracleConnection : OracleSqlConnection {

        Task<OracleConnection> GetOracleConnectionAsync(
            CancellationToken cancellationToken = default);

    }

}
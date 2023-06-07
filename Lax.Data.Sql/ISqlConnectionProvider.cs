using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Lax.Data.Sql {

    public interface ISqlConnectionProvider<TSqlConnection> where TSqlConnection : AbstractSqlConnection {

        Task<DbConnection> GetDbConnectionAsync(CancellationToken cancellationToken = default);

    }

}
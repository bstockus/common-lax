using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Lax.Data.Sql.SqlServer {

    public class SqlServerConnectionProvider<TSqlServerConnection> : ISqlServerConnectionProvider<TSqlServerConnection>,
        IDisposable
        where TSqlServerConnection : SqlServerConnection {

        private SqlConnection _sqlConnection;

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        private readonly string _connectionString;
        private readonly TimeSpan _timeOut;

        public SqlServerConnectionProvider(string connectionString, TimeSpan timeOut) {
            _connectionString = connectionString;
            _timeOut = timeOut;
        }

        public async Task<SqlConnection> GetSqlServerConnectionAsync(
            CancellationToken cancellationToken = default) {
            await _semaphoreSlim.WaitAsync(_timeOut, cancellationToken);

            try {
                if (_sqlConnection != null && (_sqlConnection.State.Equals(ConnectionState.Open) ||
                                               _sqlConnection.State.Equals(ConnectionState.Executing) ||
                                               _sqlConnection.State.Equals(ConnectionState.Fetching) ||
                                               _sqlConnection.State.Equals(ConnectionState.Connecting))) {
                    return await Task.FromResult(_sqlConnection);
                }

                _sqlConnection = new SqlConnection(_connectionString);
                await _sqlConnection.OpenAsync(cancellationToken);
                return _sqlConnection;
            } finally {
                _semaphoreSlim.Release();
            }
        }


        public async Task<DbConnection> GetDbConnectionAsync(
            CancellationToken cancellationToken = default) =>
            await GetSqlServerConnectionAsync(cancellationToken);


        public void Dispose() => _sqlConnection?.Close();

    }

}
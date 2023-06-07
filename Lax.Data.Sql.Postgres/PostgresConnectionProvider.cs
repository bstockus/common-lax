using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace Lax.Data.Sql.Postgres {

    public class
        PostgresConnectionProvider<TPostgresSqlConnection> : IPostgresConnectionProvider<TPostgresSqlConnection>,
            IDisposable
        where TPostgresSqlConnection : PostgresSqlConnection {

        private NpgsqlConnection _npgsqlConnection;

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        private readonly string _connectionString;
        private readonly TimeSpan _timeOut;


        public PostgresConnectionProvider(string connectionString, TimeSpan timeOut) {
            _connectionString = connectionString;
            _timeOut = timeOut;
        }

        public async Task<DbConnection> GetDbConnectionAsync(
            CancellationToken cancellationToken = default) =>
            await GetNpgsqlConnectionAsync(cancellationToken);

        public async Task<NpgsqlConnection> GetNpgsqlConnectionAsync(
            CancellationToken cancellationToken = default) {
            await _semaphoreSlim.WaitAsync(_timeOut, cancellationToken);

            try {
                if (_npgsqlConnection != null && (_npgsqlConnection.State.Equals(ConnectionState.Open) ||
                                                  _npgsqlConnection.State.Equals(ConnectionState.Executing) ||
                                                  _npgsqlConnection.State.Equals(ConnectionState.Fetching) ||
                                                  _npgsqlConnection.State.Equals(ConnectionState.Connecting))) {
                    return await Task.FromResult(_npgsqlConnection);
                }

                _npgsqlConnection = new NpgsqlConnection(_connectionString);
                await _npgsqlConnection.OpenAsync(cancellationToken);
                return _npgsqlConnection;
            } finally {
                _semaphoreSlim.Release();
            }
        }

        public void Dispose() => _npgsqlConnection?.Close();

    }

}
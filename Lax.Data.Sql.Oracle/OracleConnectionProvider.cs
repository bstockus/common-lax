using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Devart.Data.Oracle;

namespace Lax.Data.Sql.Oracle {

    public class OracleConnectionProvider<TOracleConnection> : IOracleConnectionProvider<TOracleConnection>, IDisposable
        where TOracleConnection : OracleSqlConnection {

        private readonly string _connectionString;
        private readonly TimeSpan _timeOut;

        private OracleConnection _oracleConnection;

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public OracleConnectionProvider(string connectionString, TimeSpan timeOut) {
            _connectionString = connectionString;
            _timeOut = timeOut;
        }

        public async Task<OracleConnection> GetOracleConnectionAsync(
            CancellationToken cancellationToken = default) {
            await _semaphoreSlim.WaitAsync(_timeOut, cancellationToken);

            try {
                if (_oracleConnection != null && (_oracleConnection.State.Equals(ConnectionState.Open) ||
                                                  _oracleConnection.State.Equals(ConnectionState.Executing) ||
                                                  _oracleConnection.State.Equals(ConnectionState.Fetching) ||
                                                  _oracleConnection.State.Equals(ConnectionState.Connecting))) {
                    return await Task.FromResult(_oracleConnection);
                }

                _oracleConnection = new OracleConnection(_connectionString);
                await _oracleConnection.OpenAsync(cancellationToken);
                return _oracleConnection;
            } finally {
                _semaphoreSlim.Release();
            }
        }

        public async Task<DbConnection> GetDbConnectionAsync(
            CancellationToken cancellationToken = default) =>
            await GetOracleConnectionAsync(cancellationToken);

        public void Dispose() => _oracleConnection?.Dispose();

    }

}
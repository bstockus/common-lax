using System;
using System.Threading.Tasks;

namespace Lax.Helpers.AsyncProgress {

    public class AsyncProgress<T> : IAsyncProgress<T> {

        private readonly Func<T, Task> _callback;

        public AsyncProgress(Func<T, Task> callback) => _callback = callback;

        public async Task Report(T value) => await _callback(value);

    }

}
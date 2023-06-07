using System.Threading.Tasks;

namespace Lax.Helpers.AsyncProgress {

    public interface IAsyncProgress<T> {

        Task Report(T value);

    }

}
using System.Threading.Tasks;

namespace Lax.Cli.Abstractions {

    public interface ICliRunner {

        string RunnerName { get; }

        Task Run(string[] args);

    }

}
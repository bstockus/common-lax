using System.Threading.Tasks;

namespace Lax.Cli.Abstractions {

    public interface ICliDecorator {

        string DecoratorName { get; }

        Task Run(string input);

    }

}
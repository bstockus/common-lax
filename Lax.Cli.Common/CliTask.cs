using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lax.Cli.Common {

    public abstract class CliTask {

        public abstract string Name { get; }

        public abstract Task Run(ILookup<string, string> args, IEnumerable<string> flags);

    }

}
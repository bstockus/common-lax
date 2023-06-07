using System;
using System.Collections.Generic;

namespace Lax.Business.Bus.Cli.TypeParsers {

    public interface IBusCliTypeParser {

        IEnumerable<Type> Types { get; }
        object Parse(IEnumerable<string> values);

    }

}
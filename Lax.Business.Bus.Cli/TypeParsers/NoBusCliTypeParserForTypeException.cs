using System;

namespace Lax.Business.Bus.Cli.TypeParsers {

    public class NoBusCliTypeParserForTypeException : Exception {

        public NoBusCliTypeParserForTypeException(Type type) : base(
            $"No Bus CLI Type Parser could be found for Type: {type.FullName}") { }

    }

}
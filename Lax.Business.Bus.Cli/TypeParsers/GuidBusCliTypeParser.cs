using System;

namespace Lax.Business.Bus.Cli.TypeParsers {

    public class GuidBusCliTypeParser : SimpleBusCliTypeParser<Guid> {

        public override Guid ParseValue(string value) => Guid.Parse(value);

    }

}
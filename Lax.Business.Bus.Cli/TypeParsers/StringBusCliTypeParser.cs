namespace Lax.Business.Bus.Cli.TypeParsers {

    public class StringBusCliTypeParser : SimpleBusCliTypeParser<string> {

        public override string ParseValue(string value) => value;

    }

}
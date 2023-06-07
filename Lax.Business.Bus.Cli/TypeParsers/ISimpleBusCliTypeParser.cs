namespace Lax.Business.Bus.Cli.TypeParsers {

    public interface ISimpleBusCliTypeParser : IBusCliTypeParser {

        object Parse(string value);

    }

}
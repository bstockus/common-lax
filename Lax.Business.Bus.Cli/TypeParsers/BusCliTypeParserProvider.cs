using System;
using System.Collections.Generic;
using System.Linq;

namespace Lax.Business.Bus.Cli.TypeParsers {

    public class BusCliTypeParserProvider {

        private readonly IDictionary<Type, Func<IEnumerable<string>, object>> _typeParsers;

        public BusCliTypeParserProvider(IEnumerable<IBusCliTypeParser> busCliTypeParsers) =>
            _typeParsers = busCliTypeParsers
                .Select(_ => _.Types.Select(x => new Tuple<Type, Func<IEnumerable<string>, object>>(x, _.Parse)))
                .SelectMany(_ => _).GroupBy(_ => _.Item1).ToDictionary(_ => _.Key, _ => _.First().Item2);

        public object Parse(Type type, IEnumerable<string> values) =>
            _typeParsers.ContainsKey(type)
                ? _typeParsers[type](values)
                : throw new NoBusCliTypeParserForTypeException(type);

    }

}
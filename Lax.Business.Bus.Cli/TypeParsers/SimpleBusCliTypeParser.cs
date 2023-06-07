using System;
using System.Collections.Generic;
using System.Linq;

namespace Lax.Business.Bus.Cli.TypeParsers {

    public abstract class SimpleBusCliTypeParser<T> : ISimpleBusCliTypeParser {

        public IEnumerable<Type> Types => new[] {
            typeof(T)
        };

        public object Parse(IEnumerable<string> values) => ParseValue(values.First());
        public object Parse(string value) => ParseValue(value);

        public abstract T ParseValue(string value);

    }

}
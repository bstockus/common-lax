using System;
using System.Collections.Generic;
using System.Linq;

namespace Lax.Business.Bus.Cli {

    public class BusRequestTypesProvider {

        private readonly IDictionary<string, Type> _requestTypesMap;

        public BusRequestTypesProvider(IEnumerable<Type> requestTypes) =>
            _requestTypesMap = requestTypes.ToDictionary(_ => _.FullName, _ => _);

        public Type GetTypeForName(string name) =>
            _requestTypesMap.ContainsKey(name)
                ? _requestTypesMap[name]
                : throw new NoBusRequestTypeForNameException(name);

    }

}
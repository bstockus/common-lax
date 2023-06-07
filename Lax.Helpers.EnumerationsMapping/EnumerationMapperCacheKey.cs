using System;

namespace Lax.Helpers.EnumerationsMapping {

    public struct EnumerationMapperCacheKey {

        public Type SourceType { get; set; }

        public Type DestinationType { get; set; }

    }

}
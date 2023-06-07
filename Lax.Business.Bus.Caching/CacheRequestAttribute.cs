using System;

namespace Lax.Business.Bus.Caching {

    [AttributeUsage(AttributeTargets.Class)]
    public class CacheRequestAttribute : Attribute {

        public TimeSpan? CacheDuration { get; }

        public CacheRequestAttribute() : this(null) { }

        public CacheRequestAttribute(TimeSpan? cacheDuration) {
            CacheDuration = cacheDuration;
        }

    }

}

using System;

namespace Lax.Business.Bus.Caching {

    [AttributeUsage(AttributeTargets.Property)]
    public class CacheRequestKeyAttribute : Attribute {

        public CacheRequestKeyAttribute() { }

    }

}
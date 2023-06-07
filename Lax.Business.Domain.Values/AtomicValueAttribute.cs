using System;

namespace Lax.Business.Domain.Values {

    [AttributeUsage(AttributeTargets.Property)]
    public class AtomicValueAttribute : Attribute {

        public AtomicValueAttribute() { }

    }

}
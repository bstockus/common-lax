using System;
using System.Collections.Generic;

namespace Lax.Helpers.AnonymousTypes.PrivateDynamicObjects {

    public class IsAssignableFromTypeEqualityComparer : IEqualityComparer<Type> {

        public bool Equals(Type x, Type y) => x != null && x.IsAssignableFrom(y);

        public int GetHashCode(Type obj) => obj.GetHashCode();

    }

}
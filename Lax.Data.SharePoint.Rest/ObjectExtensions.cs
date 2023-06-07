using System.Collections.Generic;
using System.Linq;

namespace Lax.Data.SharePoint.Rest {

    public static class ObjectExtensions {
        public static bool In<T>(this T obj, IEnumerable<T> collection) =>
            collection != null && collection.Contains(obj);

    }

}
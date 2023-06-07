using System.Collections.Generic;

namespace Lax.Helpers.Common {

    public static class ListExtensions {

        public static void Fill<T>(this IList<T> list, T value) {
            if (list.Contains(value)) {
                return;
            }

            list.Add(value);
        }

    }

}
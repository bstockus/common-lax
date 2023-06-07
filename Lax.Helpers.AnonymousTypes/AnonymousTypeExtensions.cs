using System.Collections.Generic;
using System.Dynamic;

namespace Lax.Helpers.AnonymousTypes {

    public static class AnonymousTypeExtensions {

        public static dynamic AsAnonymousType(this IDictionary<string, object> dictionary) {
            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

            foreach (var kvp in dictionary) {
                eoColl.Add(kvp);
            }

            return eo;
        }

    }

}
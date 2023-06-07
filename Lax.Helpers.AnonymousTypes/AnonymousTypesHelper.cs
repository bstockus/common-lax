using System.Collections.Generic;
using System.Dynamic;

namespace Lax.Helpers.AnonymousTypes {

    public static class AnonymousTypesHelper {

        public static dynamic Merge(object item1, object item2) {
            if (item1 == null || item2 == null) {
                return item1 ?? item2 ?? new ExpandoObject();
            }

            dynamic expando = new ExpandoObject();
            var result = expando as IDictionary<string, object>;
            foreach (var fi in item1.GetType().GetProperties()) {
                result[fi.Name] = fi.GetValue(item1, null);
            }

            foreach (var fi in item2.GetType().GetProperties()) {
                result[fi.Name] = fi.GetValue(item2, null);
            }

            return result;
        }

        public static dynamic Merge(object item1, IDictionary<string, object> dictionary) {
            dynamic expando = new ExpandoObject();
            var result = expando as IDictionary<string, object>;
            foreach (var fi in item1.GetType().GetProperties()) {
                result[fi.Name] = fi.GetValue(item1, null);
            }

            foreach (var (key, value) in dictionary) {
                result[key] = value;
            }

            return result;
        }

    }

}
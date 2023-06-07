using System;
using System.Collections.Generic;

namespace Lax.Helpers.Common {

    public static class DictionaryExtensions {

        public static void
            AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) {
            if (dictionary.ContainsKey(key)) {
                dictionary[key] = value;
            } else {
                dictionary.Add(key, value);
            }
        }

        public static void AddOrUpdateRangeUsingKeyValueSelectors<TKey, TValue, TObject>(
            this IDictionary<TKey, TValue> dictionary,
            IEnumerable<TObject> objs,
            Func<TObject, TKey> keySelector,
            Func<TObject, TValue> valueSelector) {
            foreach (var obj in objs) {
                var key = keySelector(obj);
                var value = valueSelector(obj);

                if (dictionary.ContainsKey(key)) {
                    dictionary[key] = value;
                } else {
                    dictionary.Add(key, value);
                }
            }
        }

        public static void RemoveRangeByKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IEnumerable<TKey> keys) {
            foreach (var key in keys) {
                dictionary.Remove(key);
            }
        }

    }

}
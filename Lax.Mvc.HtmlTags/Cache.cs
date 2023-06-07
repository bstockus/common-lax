using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lax.Mvc.HtmlTags {

    internal class Cache<TKey, TValue> : IEnumerable<TValue> where TValue : class {

        private readonly object _locker = new object();
        private readonly IDictionary<TKey, TValue> _values;

        public Cache()
            : this(new Dictionary<TKey, TValue>()) { }

        public Cache(Func<TKey, TValue> onMissing)
            : this(new Dictionary<TKey, TValue>(), onMissing) { }

        public Cache(IDictionary<TKey, TValue> dictionary, Func<TKey, TValue> onMissing)
            : this(dictionary) =>
            OnMissing = onMissing;

        public Cache(IDictionary<TKey, TValue> dictionary) => _values = dictionary;

        public Func<TKey, TValue> OnMissing { get; set; } = key => {
            var message = $"Key '{key}' could not be found";
            throw new KeyNotFoundException(message);
        };

        public Func<TValue, TKey> GetKey { get; set; } = arg => throw new NotImplementedException();

        public int Count => _values.Count;

        public TValue First => _values.Select(pair => pair.Value).FirstOrDefault();

        public IDictionary<TKey, TValue> Inner => _values;


        public TValue this[TKey key] {
            get {
                if (_values.ContainsKey(key)) {
                    return _values[key];
                }

                lock (_locker) {
                    if (!_values.ContainsKey(key)) {
                        var value = OnMissing(key);
                        _values.Add(key, value);
                    }
                }

                return _values[key];
            }
            set {
                if (_values.ContainsKey(key)) {
                    _values[key] = value;
                } else {
                    _values.Add(key, value);
                }
            }
        }

        #region IEnumerable<TValue> Members

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TValue>) this).GetEnumerator();

        public IEnumerator<TValue> GetEnumerator() => _values.Values.GetEnumerator();

        #endregion

        public IEnumerable<TKey> GetKeys() => _values.Keys;

        public void Fill(TKey key, TValue value) {
            if (_values.ContainsKey(key)) {
                return;
            }

            _values.Add(key, value);
        }

        public bool TryRetrieve(TKey key, out TValue value) {
            value = default;

            if (!_values.ContainsKey(key)) {
                return false;
            }

            value = _values[key];
            return true;
        }

        public void Each(Action<TValue> action) {
            foreach (var pair in _values) {
                action(pair.Value);
            }
        }

        public void Each(Action<TKey, TValue> action) {
            foreach (var pair in _values) {
                action(pair.Key, pair.Value);
            }
        }

        public bool Has(TKey key) => _values.ContainsKey(key);

        public bool Exists(Predicate<TValue> predicate) {
            var returnValue = false;

            Each(value => returnValue |= predicate(value));

            return returnValue;
        }

        public TValue Find(Predicate<TValue> predicate) => _values.Where(pair => predicate(pair.Value))
            .Select(pair => pair.Value).FirstOrDefault();

        public TValue[] GetAll() {
            var returnValue = new TValue[Count];
            _values.Values.CopyTo(returnValue, 0);

            return returnValue;
        }

        public void Remove(TKey key) {
            if (_values.ContainsKey(key)) {
                _values.Remove(key);
            }
        }

        public void ClearAll() => _values.Clear();

    }

}
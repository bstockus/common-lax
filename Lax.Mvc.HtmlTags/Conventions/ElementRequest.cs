using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Lax.Mvc.HtmlTags.Conventions.Elements;
using Lax.Mvc.HtmlTags.Conventions.Formatting;
using Lax.Mvc.HtmlTags.Reflection;

namespace Lax.Mvc.HtmlTags.Conventions {

    public class ElementRequest {

        private bool _hasFetched;
        private object _rawValue;
        private Func<Type, object> _services;

        private readonly Dictionary<string, object> _metaData = new Dictionary<string, object>();

        public static ElementRequest For(object model, PropertyInfo property) =>
            new ElementRequest(new SingleProperty(property)) {
                Model = model
            };

        public static ElementRequest For<T>(Expression<Func<T, object>> expression) =>
            new ElementRequest(expression.ToAccessor());

        public static ElementRequest For<T>(T model, Expression<Func<T, object>> expression) =>
            new ElementRequest(expression.ToAccessor()) {
                Model = model
            };

        public ElementRequest(IAccessor accessor) => Accessor = accessor;

        public object RawValue {
            get {
                if (_hasFetched) {
                    return _rawValue;
                }

                _rawValue = Model == null ? null : Accessor.GetValue(Model);
                _hasFetched = true;

                return _rawValue;
            }
        }

        public string ElementId { get; set; }
        public object Model { get; set; }
        public IAccessor Accessor { get; }
        public HtmlTag OriginalTag { get; private set; }
        public HtmlTag CurrentTag { get; private set; }

        public IDictionary<string, object> MetaData => _metaData;

        public void AddMetaData(string key, object value) {
            if (_metaData.ContainsKey(key)) {
                _metaData[key] = value;
            } else {
                _metaData.Add(key, value);
            }
        }

        public object GetMetaData(string key) {
            if (_metaData.ContainsKey(key)) {
                return _metaData[key];
            }

            return null;
        }

        public void WrapWith(HtmlTag tag) {
            CurrentTag.WrapWith(tag);
            ReplaceTag(tag);
        }

        public void ReplaceTag(HtmlTag tag) {
            if (OriginalTag == null) {
                OriginalTag = tag;
            }

            CurrentTag = tag;
        }

        public AccessorDef ToAccessorDef() => new AccessorDef(Accessor, HolderType());


        public Type HolderType() => Model == null ? Accessor.DeclaringType : Model?.GetType();

        public T Get<T>() => (T) _services(typeof(T));

        // virtual for mocking
        public virtual HtmlTag BuildForCategory(string category, string profile = null) =>
            Get<ITagGenerator>().Build(this, category, profile);

        public T Value<T>() => (T) RawValue;

        public string StringValue() =>
            new DisplayFormatter(_services).GetDisplay(new GetStringRequest(Accessor, RawValue, _services, null, null));

        public bool ValueIsEmpty() => RawValue == null || string.Empty.Equals(RawValue);

        public void ForValue<T>(Action<T> action) {
            if (ValueIsEmpty()) {
                return;
            }

            action((T) RawValue);
        }

        public void Attach(Func<Type, object> locator) => _services = locator;

        public ElementRequest ToToken() => new ElementRequest(Accessor);

    }

}
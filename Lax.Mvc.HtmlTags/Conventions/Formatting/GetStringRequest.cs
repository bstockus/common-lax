using System;
using System.Reflection;
using Lax.Helpers.Common;
using Lax.Mvc.HtmlTags.Reflection;

namespace Lax.Mvc.HtmlTags.Conventions.Formatting {

    public class GetStringRequest {

        private Type _propertyType;

        public GetStringRequest(PropertyInfo property, object rawValue)
            : this(new SingleProperty(property), rawValue, null, null, null) { }


        public GetStringRequest(IAccessor accessor, object rawValue, Func<Type, object> locator, string format,
            Type ownerType) {
            Locator = locator;
            if (accessor != null) {
                Property = accessor.InnerProperty;
            }

            RawValue = rawValue;

            if (Property != null) {
                PropertyType = Property.PropertyType;
            } else if (RawValue != null) {
                PropertyType = RawValue.GetType();
            }

            if (ownerType == null) {
                if (accessor != null) {
                    OwnerType = accessor.OwnerType;
                } else if (Property != null) {
                    OwnerType = Property.DeclaringType;
                }
            } else {
                OwnerType = ownerType;
            }

            Format = format;
        }

        public GetStringRequest(Type ownerType, PropertyInfo property, object rawValue, string format,
            Type propertyType) {
            OwnerType = ownerType;
            Property = property;
            RawValue = rawValue;
            Format = format;
            PropertyType = propertyType;
        }

        // Yes, I made this internal.  Don't necessarily want it in the public interface,
        // but needs to be "settable"
        internal Func<Type, object> Locator { get; set; }

        public Type OwnerType { get; }

        public Type PropertyType {
            get {
                if (_propertyType == null && Property != null) {
                    return Property.PropertyType;
                }

                return _propertyType;
            }
            set => _propertyType = value;
        }

        public PropertyInfo Property { get; }
        public object RawValue { get; }
        public string Format { get; }

        public string WithFormat(string format) => string.Format(format, RawValue);

        public GetStringRequest GetRequestForNullableType() =>
            new GetStringRequest(OwnerType, Property, RawValue, Format,
                PropertyType.GetInnerTypeFromNullable()) {
                Locator = Locator
            };

        public GetStringRequest GetRequestForElementType() =>
            new GetStringRequest(OwnerType, Property, RawValue, Format, PropertyType.GetElementType()) {
                Locator = Locator,
            };

        public T Get<T>() => (T) Locator(typeof(T));

        public bool Equals(GetStringRequest other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }

            if (ReferenceEquals(this, other)) {
                return true;
            }

            return other.OwnerType == OwnerType && Equals(other.Property, Property) &&
                   Equals(other.RawValue, RawValue) && Equals(other.Format, Format);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            return obj.GetType() == typeof(GetStringRequest) && Equals((GetStringRequest) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var result = OwnerType?.GetHashCode() ?? 0;
                result = (result * 397) ^ (Property?.GetHashCode() ?? 0);
                result = (result * 397) ^ (RawValue?.GetHashCode() ?? 0);
                result = (result * 397) ^ (Format?.GetHashCode() ?? 0);
                return result;
            }
        }

    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Lax.Helpers.Common {

    public static class EnumerationExtensions {

        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Any()) ? (T) attributes.First() : null;
        }

        public static IEnumerable<T> GetAttributesOfType<T>(this Enum enumVal) where T : Attribute {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.AsEnumerable().Cast<T>();
        }

        public static string GetEnumDescription(this Enum value) {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

    }

}
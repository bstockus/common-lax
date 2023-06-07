using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Lax.Mvc.AdminLte.Bootstrap.Attributes;

namespace Lax.Mvc.AdminLte.Bootstrap.Extensions {

    public static class EnumExtensions {

        public static string GetDescription<T>(this T enumerationValue) where T : struct =>
            enumerationValue.GetAttribute<DisplayValueAttribute, T>()?
                .Name ?? enumerationValue.GetAttribute<DisplayAttribute, T>()?.Name ?? enumerationValue.ToString();

        public static string GetDisplayValue<T>(this T enumerationValue) where T : struct =>
            enumerationValue.GetAttribute<DefaultValueAttribute, T>()?
                .Value?.ToString() ?? enumerationValue.ToString();

        public static T GetAttribute<T, TS>(this TS enumerationValue) where T : Attribute where TS : struct {
            var type = enumerationValue.GetType();
            if (!type.GetTypeInfo().IsEnum) {
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(enumerationValue));
            }

            return
                type.GetMember(enumerationValue.ToString())
                    .FirstOrDefault()?
                    .GetCustomAttribute<T>();
        }

        public static IEnumerable<T> GetAttributes<T, TS>(this TS enumerationValue)
            where T : Attribute where TS : struct {
            var type = enumerationValue.GetType();
            if (!type.GetTypeInfo().IsEnum) {
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(enumerationValue));
            }

            return
                type.GetMember(enumerationValue.ToString())
                    .FirstOrDefault()?
                    .GetCustomAttributes<T>();
        }

    }

}
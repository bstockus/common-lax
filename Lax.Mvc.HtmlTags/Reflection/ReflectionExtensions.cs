using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Lax.Helpers.Common;

namespace Lax.Mvc.HtmlTags.Reflection {

    public static class ReflectionExtensions {

        public static TU ValueOrDefault<T, TU>(this T root, Expression<Func<T, TU>> expression)
            where T : class {
            if (root == null) {
                return default;
            }

            var accessor = ReflectionHelper.GetAccessor(expression);

            var result = accessor.GetValue(root);

            return (TU) (result ?? default(TU));
        }

        public static IEnumerable<T> GetAllAttributes<T>(this IAccessor accessor) where T : Attribute =>
            accessor.InnerProperty.GetCustomAttributes<T>();

        public static void ForAttribute<T>(this IAccessor accessor, Action<T> action) where T : Attribute {
            foreach (T attribute in accessor.InnerProperty.GetCustomAttributes(typeof(T), true)) {
                action(attribute);
            }
        }

        public static T GetAttribute<T>(this IAccessor provider) where T : Attribute =>
            provider.InnerProperty.GetCustomAttribute<T>();

        public static bool HasAttribute<T>(this IAccessor provider) where T : Attribute =>
            provider.InnerProperty.GetCustomAttribute<T>() != null;

        public static IAccessor ToAccessor<T>(this Expression<Func<T, object>> expression) =>
            ReflectionHelper.GetAccessor(expression);

        public static string GetName<T>(this Expression<Func<T, object>> expression) =>
            ReflectionHelper.GetAccessor(expression).Name;


        public static void IfPropertyTypeIs<T>(this IAccessor accessor, Action action) {
            if (accessor.PropertyType == typeof(T)) {
                action();
            }
        }

        public static bool IsInteger(this IAccessor accessor) =>
            accessor.PropertyType.IsTypeOrNullableOf<int>() || accessor.PropertyType.IsTypeOrNullableOf<long>();

    }

}
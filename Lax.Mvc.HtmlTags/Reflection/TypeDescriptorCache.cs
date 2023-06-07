using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection {

    public class TypeDescriptorCache : ITypeDescriptorCache {

        private static readonly Cache<Type, IDictionary<string, PropertyInfo>> Cache;

        static TypeDescriptorCache() =>
            Cache = new Cache<Type, IDictionary<string, PropertyInfo>>(type =>
                type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(propertyInfo => propertyInfo.CanWrite)
                    .ToDictionary(propertyInfo => propertyInfo.Name));

        public IDictionary<string, PropertyInfo> GetPropertiesFor<T>() => GetPropertiesFor(typeof(T));

        public IDictionary<string, PropertyInfo> GetPropertiesFor(Type itemType) => Cache[itemType];

        public void ForEachProperty(Type itemType, Action<PropertyInfo> action) => Cache[itemType].Values.Each(action);

        public void ClearAll() => Cache.ClearAll();

        public static PropertyInfo GetPropertyFor(Type modelType, string propertyName) {
            var dict = Cache[modelType];
            return dict.ContainsKey(propertyName) ? dict[propertyName] : null;
        }

    }

}
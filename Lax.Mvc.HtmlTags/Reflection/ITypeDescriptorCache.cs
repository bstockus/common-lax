using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection {

    public interface ITypeDescriptorCache {

        IDictionary<string, PropertyInfo> GetPropertiesFor<T>();
        IDictionary<string, PropertyInfo> GetPropertiesFor(Type itemType);
        void ForEachProperty(Type itemType, Action<PropertyInfo> action);
        void ClearAll();

    }

}
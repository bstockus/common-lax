using System;
using Lax.Mvc.HtmlTags.Reflection;

namespace Lax.Mvc.HtmlTags.Conventions.Elements {

    public class DefaultElementNamingConvention : IElementNamingConvention {

        public string GetName(Type modelType, IAccessor accessor) => accessor.Name;

    }

}
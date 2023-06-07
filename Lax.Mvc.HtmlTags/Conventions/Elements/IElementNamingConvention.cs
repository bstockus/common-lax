using System;
using Lax.Mvc.HtmlTags.Reflection;

namespace Lax.Mvc.HtmlTags.Conventions.Elements {

    public interface IElementNamingConvention {

        string GetName(Type modelType, IAccessor accessor);

    }

}
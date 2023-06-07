using System.Linq;

namespace Lax.Mvc.HtmlTags.Reflection {

    public static class AccessorExtensions {

        public static IAccessor Prepend(this IAccessor accessor, IAccessor prefixedAccessor) =>
            new PropertyChain(prefixedAccessor.Getters().Union(accessor.Getters()).ToArray());

    }

}
using System;
using System.Linq;
using Lax.Mvc.HtmlTags.Reflection;

namespace Lax.Mvc.HtmlTags.Conventions.Elements {

    public class DotNotationElementNamingConvention : IElementNamingConvention {

        public static Func<string, bool> IsCollectionIndexer = x => x.StartsWith("[") && x.EndsWith("]");

        public string GetName(Type modelType, IAccessor accessor) =>
            accessor.PropertyNames
                .Aggregate((x, y) => {
                    var formatString = IsCollectionIndexer(y)
                        ? "{0}{1}"
                        : "{0}.{1}";
                    return string.Format(formatString, new object[] {x, y});
                });

    }

}
using System.Collections.Generic;

namespace Lax.Mvc.HtmlTags {

    public static class HtmlTagExtensions {

        public static TagList ToTagList(this IEnumerable<HtmlTag> tags) => new TagList(tags);

    }

}
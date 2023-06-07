namespace Lax.Mvc.HtmlTags {

    public static class LiteralTagExtensions {

        /// <summary>
        ///     Adds a LiteralTag to the Children collection
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlTag AppendHtml(this HtmlTag tag, string html) => tag.Append(new LiteralTag(html));

    }

}
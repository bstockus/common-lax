using System.IO;
using System.Text.Encodings.Web;

namespace Lax.Mvc.HtmlTags {

    /// <summary>
    ///     HtmlTag that *only outputs the literal html put into it in the
    ///     constructor function
    /// </summary>
    public class LiteralTag : HtmlTag {

        public LiteralTag(string html) : base("div") {
            Text(html);
            Encoded(false);
        }

        protected override void WriteHtml(TextWriter html, HtmlEncoder encoder) => html.Write(Text());

    }

}
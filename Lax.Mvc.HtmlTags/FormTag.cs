namespace Lax.Mvc.HtmlTags {

    public class FormTag : HtmlTag {

        public FormTag(string url) : this() => Action(url);

        public FormTag() : base("form") => Method("post");

        public FormTag Method(string httpMethod) {
            Attr("method", httpMethod);
            return this;
        }

        public FormTag Action(string url) {
            Attr("action", url);
            return this;
        }

    }

}
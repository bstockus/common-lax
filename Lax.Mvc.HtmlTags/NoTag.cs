namespace Lax.Mvc.HtmlTags {

    public class NoTag : HtmlTag {

        public NoTag() : base("") => Render(false);

    }

}
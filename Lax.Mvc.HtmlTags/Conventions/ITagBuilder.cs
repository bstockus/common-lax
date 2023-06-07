namespace Lax.Mvc.HtmlTags.Conventions {

    public interface ITagBuilder {

        HtmlTag Build(ElementRequest request);

    }

}
namespace Lax.Mvc.HtmlTags.Conventions {

    public interface ITagPlan {

        HtmlTag Build(ElementRequest request);

    }

}
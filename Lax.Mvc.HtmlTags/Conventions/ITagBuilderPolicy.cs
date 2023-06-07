namespace Lax.Mvc.HtmlTags.Conventions {

    public interface ITagBuilderPolicy {

        bool Matches(ElementRequest subject);
        ITagBuilder BuilderFor(ElementRequest subject);

    }

}
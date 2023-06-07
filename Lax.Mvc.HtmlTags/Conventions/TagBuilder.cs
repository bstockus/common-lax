namespace Lax.Mvc.HtmlTags.Conventions {

    public abstract class TagBuilder : ITagBuilderPolicy, ITagBuilder {

        public abstract bool Matches(ElementRequest subject);

        public ITagBuilder BuilderFor(ElementRequest subject) => this;

        public abstract HtmlTag Build(ElementRequest request);

    }

}
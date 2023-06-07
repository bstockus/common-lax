namespace Lax.Mvc.HtmlTags.Conventions.Elements {

    public interface IElementTagOverride {

        string Category { get; }
        string Profile { get; }
        IElementBuilder Builder();

    }

}
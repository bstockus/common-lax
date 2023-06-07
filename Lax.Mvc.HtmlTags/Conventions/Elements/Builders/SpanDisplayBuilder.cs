namespace Lax.Mvc.HtmlTags.Conventions.Elements.Builders {

    public class SpanDisplayBuilder : IElementBuilder {

        public HtmlTag Build(ElementRequest request) =>
            new HtmlTag("span").Text(request.StringValue()).Id(request.ElementId);

    }

}
using Lax.Mvc.HtmlTags.Conventions.Elements;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.HtmlTags {

    [HtmlTargetElement("label-tag", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class LabelTagHelper : HtmlTagTagHelper {

        protected override string Category { get; } = ElementConstants.Label;

    }

}
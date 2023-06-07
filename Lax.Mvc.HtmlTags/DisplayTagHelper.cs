using Lax.Mvc.HtmlTags.Conventions.Elements;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.HtmlTags {

    [HtmlTargetElement("display-tag", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class DisplayTagHelper : HtmlTagTagHelper {

        protected override string Category { get; } = ElementConstants.Display;

    }

}
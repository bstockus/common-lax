using Lax.Mvc.HtmlTags.Conventions.Elements;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.HtmlTags {

    [HtmlTargetElement("input-tag", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class InputTagHelper : HtmlTagTagHelper {

        protected override string Category { get; } = ElementConstants.Editor;

    }

}
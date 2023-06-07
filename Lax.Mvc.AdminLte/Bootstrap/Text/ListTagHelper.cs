using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Text {

    [HtmlTargetElement("ul", Attributes = UnstyledAttributeName)]
    [HtmlTargetElement("ul", Attributes = InlineAttributeName)]
    [HtmlTargetElement("ol", Attributes = UnstyledAttributeName)]
    [HtmlTargetElement("ol", Attributes = InlineAttributeName)]
    public class ListTagHelper : BootstrapTagHelper {

        public const string InlineAttributeName = AttributePrefix + "inline";
        public const string UnstyledAttributeName = AttributePrefix + "unstyled";

        [HtmlAttributeName(UnstyledAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Unstyled { get; set; }

        [HtmlAttributeName(InlineAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Inline { get; set; }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            if (Unstyled) {
                output.AddCssClass("list-unstyled");
            }

            if (Inline) {
                output.AddCssClass("list-inline");
            }
        }

    }

}
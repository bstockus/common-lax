using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Text {

    [HtmlTargetElement("pre", Attributes = ScrollableAttributeName)]
    public class PreTagHelper : BootstrapTagHelper {

        public const string ScrollableAttributeName = AttributePrefix + "scrollable";

        [HtmlAttributeName(ScrollableAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Scrollable { get; set; }


        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            if (Scrollable) {
                output.AddCssClass("pre-scrollable");
            }
        }

    }

}
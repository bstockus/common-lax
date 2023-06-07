using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap {

    [OutputElementHint("ul")]
    [RestrictChildren("nav-item", "dropdown")]
    [ContextClass]
    public class NavTabsTagHelper : BootstrapTagHelper {

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool Justified { get; set; }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "ul";
            output.AddCssClass("nav");
            output.AddCssClass("nav-tabs");
            if (Justified) {
                output.AddCssClass("nav-justified");
            }
        }

    }

}
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.ResponsiveUtilities {

    [OutputElementHint("div")]
    public class VisibleXsTagHelper : BootstrapTagHelper {

        [HtmlAttributeName(VisibleLgTagHelper.DisplayModeAttributeName)]
        public BootstrapResponsiveUtilitiesDisplayMode DisplayMode { get; set; } =
            BootstrapResponsiveUtilitiesDisplayMode.Block;

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = DisplayMode == BootstrapResponsiveUtilitiesDisplayMode.Inline ? "span" : "div";
            output.AddCssClass("visible-xs-" + DisplayMode.GetDescription());
        }

    }

}
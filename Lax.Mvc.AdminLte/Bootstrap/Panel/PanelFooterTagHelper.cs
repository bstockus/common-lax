using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Panel {

    [OutputElementHint("div")]
    [HtmlTargetElement("footer", ParentTag = "panel")]
    public class PanelFooterTagHelper : BootstrapTagHelper {

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "div";
            output.AddCssClass("panel-footer");
        }

    }

}
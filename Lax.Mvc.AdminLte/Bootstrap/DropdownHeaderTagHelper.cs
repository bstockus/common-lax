using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap {

    [HtmlTargetElement("header", ParentTag = "dropdown")]
    [OutputElementHint("li")]
    public class DropdownHeaderTagHelper : BootstrapTagHelper {

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "li";
            output.AddCssClass("dropdown-header");
        }

    }

}
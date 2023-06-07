using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Forms {

    [OutputElementHint("span")]
    public class HelpBlockTagHelper : BootstrapTagHelper {

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "span";
            output.AddCssClass("help-block");
            output.AddCssClass("with-errors");
        }

        public static string GenerateHelpBlock(string helpContent) =>
            "<span class=\"help-block\">" + helpContent + "</span>";

    }

}
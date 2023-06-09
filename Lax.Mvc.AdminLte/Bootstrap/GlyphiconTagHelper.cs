using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap {

    [OutputElementHint("span")]
    [HtmlTargetElement("glyphicon", Attributes = "icon")]
    public class GlyphiconTagHelper : BootstrapTagHelper {

        public Glyphicons Icon { get; set; }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "span";
            output.AddCssClass("glyphicon");
            output.AddCssClass(Icon.GetDescription());
        }

    }

}
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap {

    [HtmlTargetElement(ParentTag = "responsive-embed")]
    public class ResponsiveEmbedChildrenTagHelper : BootstrapTagHelper {

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) =>
            output.AddCssClass("embed-responsive-item");

    }

}
using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Media {

    [OutputElementHint("ul")]
    [RestrictChildren("media")]
    [ContextClass]
    public class MediaListTagHelper : BootstrapTagHelper {

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "ul";
            output.AddCssClass("media-list");
        }

    }

}
using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Navigation {

    [OutputElementHint("ul")]
    [RestrictChildren("nav-item", "dropdown")]
    [ContextClass]
    public class NavbarNavTagHelper : BootstrapTagHelper {

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "ul";
            output.AddCssClass("nav");
            output.AddCssClass("navbar-nav");
        }

    }

}
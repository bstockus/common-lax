using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Navigation {

    [HtmlTargetElement("form", ParentTag = "navbar")]
    public class NavbarFormTagHelper : BootstrapTagHelper {

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) =>
            output.AddCssClass("navbar-form");

    }

}
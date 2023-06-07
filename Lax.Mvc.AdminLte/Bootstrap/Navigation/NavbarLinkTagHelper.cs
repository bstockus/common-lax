using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Navigation {

    [HtmlTargetElement("a", Attributes = NavbarLinkAttributeName)]
    public class NavbarLinkTagHelper : BootstrapTagHelper {

        public const string NavbarLinkAttributeName = AttributePrefix + "navbar-link";

        [HtmlAttributeName(NavbarLinkAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool NavbarLink { get; set; }


        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) =>
            output.AddCssClass("navbar-link");

    }

}
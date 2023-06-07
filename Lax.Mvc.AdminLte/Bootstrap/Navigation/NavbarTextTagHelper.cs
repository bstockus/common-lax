using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Navigation {

    [HtmlTargetElement("p", ParentTag = "navbar")]
    [HtmlTargetElement("*", ParentTag = "navbar", Attributes = NavbarTextAttributeName)]
    public class NavbarTextTagHelper : BootstrapTagHelper {

        private const string NavbarTextAttributeName = AttributePrefix + "navbar-text";

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        [HtmlAttributeName(NavbarTextAttributeName)]
        public bool NavbarText { get; set; }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) =>
            output.AddCssClass("navbar-text");

    }

}
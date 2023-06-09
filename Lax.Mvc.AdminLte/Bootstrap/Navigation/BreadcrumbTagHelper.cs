using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Navigation {

    [HtmlTargetElement(ParentTag = "breadcrumbs")]
    [OutputElementHint("li")]
    public class BreadcrumbTagHelper : BootstrapTagHelper {

        public string Href { get; set; }

        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Active { get; set; }


        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "li";
            if (Active) {
                output.AddCssClass("active");
            }

            if (!string.IsNullOrEmpty(Href)) {
                output.PreContent.SetHtmlContent($"<a href=\"{Href}\">");
                output.PostContent.SetHtmlContent("</a>");
            }
        }

    }

}
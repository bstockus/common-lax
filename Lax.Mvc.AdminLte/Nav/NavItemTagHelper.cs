using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Nav {

    [HtmlTargetElement("lte-nav-item")]
    public class NavItemTagHelper : TagHelper {

        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        [HtmlAttributeName("asp-page")]
        public string PagePath { get; set; }

        [HtmlAttributeName("namespace")]
        public string Namespace { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public NavItemTagHelper(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "li";
            output.TagMode = TagMode.StartTagAndEndTag;

            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

            var url = "";

            if (!string.IsNullOrWhiteSpace(PagePath)) {
                url = urlHelper.Page(PagePath);
            }

            if (_actionContextAccessor.ActionContext.HttpContext.Request.Path.StartsWithSegments(
                new PathString(Namespace))) {
                output.Attributes.SetAttribute("class", "active");
            }

            output.PreContent.SetHtmlContent($"<a href='{url}'>");
            output.PostContent.SetHtmlContent("</a>");
        }

    }

}
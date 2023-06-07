using System.Threading.Tasks;
using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Navigation {

    [OutputElementHint("a")]
    [HtmlTargetElement("brand", ParentTag = "navbar")]
    public class BrandTagHelper : BootstrapTagHelper {

        public BrandTagHelper(IActionContextAccessor actionContextAccessor) =>
            ActionContextAccessor = actionContextAccessor;

        [CopyToOutput]
        [ConvertVirtualUrl]
        public string Href { get; set; }

        [HtmlAttributeNotBound]
        [Context]
        public NavbarTagHelper NavbarContext { get; set; }

        protected override async Task BootstrapProcessAsync(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "a";
            output.AddCssClass("navbar-brand");
            IHtmlContent tagHelperContent = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(tagHelperContent);
            NavbarContext.BrandContent = output.ToTagHelperContent().GetContent();
            output.SuppressOutput();
        }

    }

}
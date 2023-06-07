using System.Threading.Tasks;
using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Modal {

    [HtmlTargetElement("footer", ParentTag = "bs-modal")]
    [OutputElementHint("div")]
    public class ModalFooterTagHelper : BootstrapTagHelper {

        [HtmlAttributeNotBound]
        [Context]
        public ModalTagHelper ModalContext { get; set; }

        protected override async Task BootstrapProcessAsync(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "div";
            output.AddCssClass("modal-footer");
            await output.LoadChildContentAsync();
            ModalContext.FooterHtml = output.ToTagHelperContent().GetContent();
            output.SuppressOutput();
        }

    }

}
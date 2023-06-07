using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Modal {

    [HtmlTargetElement("*", Attributes = DismissModalAttributeName)]
    public class DismissModalTagHelper : BootstrapTagHelper {

        public const string DismissModalAttributeName = AttributePrefix + "dismiss-modal";

        [HtmlAttributeName(DismissModalAttributeName)]
        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool DismissModal { get; set; } = true;

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            if (DismissModal) {
                output.Attributes.AddDataAttribute("dismiss", "modal");
            }
        }

    }

}
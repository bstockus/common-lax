using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Text {

    [HtmlTargetElement("dl", Attributes = HorizonzalAttributeName)]
    public class DlTagHelper : BootstrapTagHelper {

        private const string HorizonzalAttributeName = AttributePrefix + "horizontal";

        [HtmlAttributeName(HorizonzalAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Horizontal { get; set; }


        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            if (Horizontal) {
                output.AddCssClass("dl-horizontal");
            }
        }

    }

}
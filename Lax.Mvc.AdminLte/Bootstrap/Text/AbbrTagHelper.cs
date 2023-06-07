using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Text {

    [HtmlTargetElement("abbr", Attributes = InitialismAttributeName)]
    public class AbbrTagHelper : BootstrapTagHelper {

        public const string InitialismAttributeName = AttributePrefix + "initialism";

        [HtmlAttributeName(InitialismAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Initialism { get; set; }


        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            if (Initialism) {
                output.AddCssClass("initialism");
            }
        }

    }

}
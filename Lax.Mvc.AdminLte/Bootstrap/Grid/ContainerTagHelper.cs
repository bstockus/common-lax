using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class ContainerTagHelper : BootstrapTagHelper {

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool Fluid { get; set; }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "div";
            output.AddCssClass(Fluid ? "container-fluid" : "container");
        }

    }

}
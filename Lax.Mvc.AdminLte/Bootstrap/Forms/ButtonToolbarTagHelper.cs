using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Forms {

    [RestrictChildren("button", "a", "dropdown", "button-group")]
    [OutputElementHint("div")]
    [ContextClass]
    public class ButtonToolbarTagHelper : BootstrapTagHelper {

        public Size? Size { get; set; }
        public ButtonContext? Context { get; set; }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "div";
            output.Attributes.Add("role", "toolbar");
            output.AddCssClass("btn-toolbar");
        }

    }

}
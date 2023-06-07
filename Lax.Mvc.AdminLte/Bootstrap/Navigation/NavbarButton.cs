using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Navigation {

    [HtmlTargetElement("button", ParentTag = "navbar")]
    [HtmlTargetElement("input", Attributes = TypeAttributeName, ParentTag = "navbar")]
    [HtmlTargetElement("a", Attributes = ButtonAttributeName, ParentTag = "navbar")]
    public class NavbarButton : BootstrapTagHelper {

        public const string TypeAttributeName = "type";
        public const string ButtonAttributeName = AttributePrefix + "button";

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        [HtmlAttributeName(ButtonAttributeName)]
        public bool Button { get; set; }

        [CopyToOutput]
        public string Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            Type = Type?.ToLower() ?? "";
            output.TagName = output.TagName.ToLower();
            if (Button || output.TagName == "button" ||
                output.TagName == "input" && (Type == "button" || Type == "submit" || Type == "reset")) {
                base.Process(context, output);
            }
        }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) =>
            output.AddCssClass("navbar-btn");

    }

}
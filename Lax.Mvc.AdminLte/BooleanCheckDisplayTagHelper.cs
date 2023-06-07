using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte {

    [HtmlTargetElement("lax-cbo")]
    public class BooleanCheckDisplayTagHelper : TagHelper {

        [HtmlAttributeName("value")]
        public bool Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "i";
            output.Attributes.SetAttribute("class", Value ? "fa fa-check-square-o" : "fa fa-square-o");
        }

    }

}
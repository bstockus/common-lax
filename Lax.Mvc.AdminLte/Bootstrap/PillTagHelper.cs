using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap {

    [HtmlTargetElement("bs-pill")]
    public class PillTagHelper : TagHelper {

        [HtmlAttributeName("title")]
        public string Title { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.PreContent.SetHtmlContent(
                $"<span class='label-pill-title'>{Title}</span><span class='label-pill-value'>");
            output.PostContent.SetHtmlContent("</span>");

            output.Attributes.SetAttribute("class", "label label-default label-pill");
            output.Attributes.SetAttribute("style", "margin-right: 2px; margin-bottom: 2px;");
        }

    }

}
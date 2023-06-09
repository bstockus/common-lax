using System.Threading.Tasks;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap {

    [OutputElementHint("div")]
    public class ThumbnailTagHelper : BootstrapTagHelper {

        public string Href { get; set; }
        public string Src { get; set; }
        public string Alt { get; set; }


        protected override async Task BootstrapProcessAsync(TagHelperContext context, TagHelperOutput output) {
            output.TagName = Href == null ? "div" : "a";
            output.AddCssClass("thumbnail");
            output.TagMode = TagMode.StartTagAndEndTag;
            output.PreContent.AppendHtml($"<img src=\"{Src}\" alt=\"{Alt}\" />");
            if (Href != null) {
                output.Attributes.Add("href", Href);
            }

            var content = (await output.GetChildContentAsync(true));
            if (!content.IsEmptyOrWhiteSpace) {
                output.PreContent.AppendHtml("<div class=\"caption\">");
                output.PostContent.SetHtmlContent("</div>");
            }
        }

    }

}
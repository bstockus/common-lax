using Lax.Serialization.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace Lax.Mvc.AdminLte {

    [HtmlTargetElement("json-data")]
    public class JsonDataConstantTagHelper : TagHelper {

        [HtmlAttributeName("name")]
        public string Name { get; set; }

        [HtmlAttributeName("data")]
        public object Data { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("type", "text/javascript");

            var serializer = ViewContext.HttpContext.RequestServices.GetService<IJsonSerializier>();

            output.Content.SetHtmlContent($"var {Name} = {serializer.Serialize(Data)};");
        }

    }

}
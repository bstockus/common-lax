using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Forms {

    [HtmlTargetElement("lte-form-select-input")]
    public class FormSelectTagHelper : TagHelper {

        [HtmlAttributeName("title")]
        public string Title { get; set; }

        [HtmlAttributeName("remote-api")]
        public string RemoteApi { get; set; }

        [HtmlAttributeName("item")]
        public ISelectListItem Item { get; set; }

        [HtmlAttributeName("form-name")]
        public string FormName { get; set; }

        [HtmlAttributeName("placeholder")]
        public string Placeholder { get; set; } = "";

        [HtmlAttributeName("not-required")]
        public bool NotRequired { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            var results = new StringBuilder();

            results.Append(
                $"<label for='{FormName}'>{Title}</label><select class='form-control select2-remote' data-api='{RemoteApi}' data-placeholder='{Placeholder}' data-not-required='{NotRequired}' " +
                $"name='{FormName}' id='{FormName}' style='width: 100%;'>");

            if (NotRequired) {
                results.Append("<option></option>");
            }

            results.Append(Item == null ? "" : $"<option value='{Item.Id}' selected='selected'>{Item.Name}</option>");
            results.Append("</select>");

            output.Attributes.SetAttribute("class", "form-group");

            output.Content.SetHtmlContent(results.ToString());
        }

    }

}
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Forms {

    [HtmlTargetElement("lte-form-multi-select-input")]
    public class FormMultiSelectTagHelper : TagHelper {

        [HtmlAttributeName("title")]
        public string Title { get; set; }

        [HtmlAttributeName("remote-api")]
        public string RemoteApi { get; set; }

        [HtmlAttributeName("items")]
        public IEnumerable<ISelectListItem> Items { get; set; } = new List<ISelectListItem>();

        [HtmlAttributeName("form-name")]
        public string FormName { get; set; }

        [HtmlAttributeName("placeholder")]
        public string Placeholder { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            var results = new StringBuilder();

            results.Append(
                $"<label for='{FormName}'>{Title}</label><select class='form-control select2-remote-multiple' data-api='{RemoteApi}' data-placeholder='{Placeholder}' " +
                $"name='{FormName}' id='{FormName}' style='width: 100%;'>");

            results.Append(Items.Any()
                ? string.Concat(Items.OrderBy(v => v.Name)
                    .Select(v => $"<option value='{v.Id}' selected='selected'>{v.Name}</option>"))
                : "");
            results.Append("</select>");

            output.Attributes.SetAttribute("class", "form-group");

            output.Content.SetHtmlContent(results.ToString());
        }

    }

}
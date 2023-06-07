using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Forms {

    [HtmlTargetElement("lte-form-text-input")]
    public class FormTextInputTagHelper : TagHelper {

        [HtmlAttributeName("field")]
        public string Field { get; set; }

        [HtmlAttributeName("placeholder")]
        public string Placeholder { get; set; }

        [HtmlAttributeName("title")]
        public string Title { get; set; }

        [HtmlAttributeName("value")]
        public string Value { get; set; }

        [HtmlAttributeName("help-text")]
        public string HelpText { get; set; } = "";

        [HtmlAttributeName("is-required")]
        public bool IsRequired { get; set; } = false;

        [HtmlAttributeName("max-length")]
        public int? MaxLength { get; set; }

        [HtmlAttributeName("min-length")]
        public int? MinLength { get; set; }

        [HtmlAttributeName("is-multi")]
        public bool IsMulti { get; set; } = false;

        [HtmlAttributeName("field-type")]
        public string FieldType { get; set; } = "text";

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", "has-feedback form-group");

            var results = new StringBuilder();

            results.Append($"<label for='{Field}'>{Title}</label>");

            if (IsMulti) {
                results.Append("<textarea ");
            } else {
                results.Append($"<input type='{FieldType}' ");
            }

            results.Append(
                $"id='{Field}' name='{Field}' value='{(Value ?? "")}' class='form-control' placeholder='{Placeholder}' ");

            if (IsRequired) {
                results.Append("required ");
            }

            if (MaxLength.HasValue) {
                results.Append($"maxlength='{MaxLength.Value}' ");
            }

            if (MinLength.HasValue) {
                results.Append($"minlength='{MinLength.Value}' ");
            }

            if (IsMulti) {
                results.Append($">{Value ?? ""}</textarea>");
            } else {
                results.Append("/>");
            }

            results.Append($"<div class='help-block with-errors'>{HelpText}</div>");

            output.Content.SetHtmlContent(results.ToString());
        }

    }

}
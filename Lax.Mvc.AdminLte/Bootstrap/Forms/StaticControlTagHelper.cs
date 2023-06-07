using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Forms {

    [OutputElementHint("p")]
    public class StaticControlTagHelper : BootstrapTagHelper {

        protected override bool CopyAttributesIfBootstrapIsDisabled => true;

        [CopyToOutput]
        public string Id { get; set; }

        public string Label { get; set; }
        public string HelpText { get; set; }

        [HtmlAttributeNotBound]
        [Context]
        public FormTagHelper FormContext { get; set; }

        [HtmlAttributeNotBound]
        [Context]
        public FormGroupTagHelper FormGroupContext { get; set; }

        public Size? Size { get; set; }

        public override void Init(TagHelperContext context) {
            base.Init(context);
            Size ??= FormContext?.ControlSize;
        }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "p";
            output.AddCssClass("form-control-static");
            if (FormGroupContext != null) {
                FormGroupContext.WrapInDivForHorizontalForm(output, !string.IsNullOrEmpty(Label));
            } else {
                FormContext?.WrapInDivForHorizontalForm(output, !string.IsNullOrEmpty(Label));
            }

            if (!string.IsNullOrEmpty(Label)) {
                output.PreElement.Prepend(LabelTagHelper.GenerateLabel(Label, FormContext));
            }

            if (!string.IsNullOrEmpty(HelpText)) {
                output.PostElement.PrependHtml(HelpBlockTagHelper.GenerateHelpBlock(HelpText));
            }

            if (Size != null && Size != Bootstrap.Size.Default) {
                output.AddCssClass("input-" + Size.Value.GetDescription());
            }
        }

    }

}
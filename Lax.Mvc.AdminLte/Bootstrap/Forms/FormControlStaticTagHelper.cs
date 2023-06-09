using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Forms {

    [OutputElementHint("p")]
    public class FormControlStaticTagHelper : BootstrapTagHelper {

        public string Label { get; set; }

        [HtmlAttributeNotBound]
        [Context]
        public FormTagHelper FormContext { get; set; }

        [HtmlAttributeNotBound]
        public FormGroupTagHelper FormGroupContext { get; set; }

        public override void Init(TagHelperContext context) => base.Init(context);

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "p";
            output.AddCssClass("form-control-static");
            if (!string.IsNullOrEmpty(Label)) {
                output.PreElement.Prepend(LabelTagHelper.GenerateLabel(Label, FormContext));
            }

            if (FormGroupContext != null) {
                FormGroupContext.WrapInDivForHorizontalForm(output, !string.IsNullOrEmpty(Label));
            } else {
                FormContext?.WrapInDivForHorizontalForm(output, !string.IsNullOrEmpty(Label));
            }
        }

    }

}
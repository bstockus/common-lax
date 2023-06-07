using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Forms {

    [HtmlTargetElement("label-container", ParentTag = "form")]
    [HtmlTargetElement("label-container", ParentTag = "form-group")]
    [OutputElementHint("div")]
    public class LabelContainerTagHelper : HorizontalFormContainerTagHelper {

        public override void Init(TagHelperContext context) {
            base.Init(context);
            if (FormGroupContext != null) {
                FormGroupContext.HasLabel = true;
            }

            WidthLg ??= FormGroupContext?.LabelWidthLg ?? FormContext?.LabelWidthLg;
            WidthMd ??= FormGroupContext?.LabelWidthMd ?? FormContext?.LabelWidthMd;
            WidthSm ??= FormGroupContext?.LabelWidthSm ?? FormContext?.LabelWidthSm;
            WidthXs ??= FormGroupContext?.LabelWidthXs ?? FormContext?.LabelWidthXs;
        }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            base.BootstrapProcess(context, output);

            if (FormContext?.Horizontal ?? true) {
                output.AddCssClass("control-label");
            }
        }

    }

}
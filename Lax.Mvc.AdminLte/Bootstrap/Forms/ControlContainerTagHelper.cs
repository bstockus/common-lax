using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Forms {

    [HtmlTargetElement("control-container", ParentTag = "form")]
    [HtmlTargetElement("control-container", ParentTag = "form-group")]
    [OutputElementHint("div")]
    public class ControlContainerTagHelper : HorizontalFormContainerTagHelper {

        public override void Init(TagHelperContext context) {
            base.Init(context);
            WidthLg ??= 12 -
                        (FormGroupContext?.LabelWidthLg ??
                         (FormContext?.LabelWidthLg != 0 ? FormContext?.LabelWidthLg : 12));
            WidthMd ??= 12 -
                        (FormGroupContext?.LabelWidthMd ??
                         (FormContext?.LabelWidthMd != 0 ? FormContext?.LabelWidthMd : 12));
            WidthSm ??= 12 -
                        (FormGroupContext?.LabelWidthSm ??
                         (FormContext?.LabelWidthSm != 0 ? FormContext?.LabelWidthSm : 12));
            WidthXs ??= 12 -
                        (FormGroupContext?.LabelWidthXs ??
                         (FormContext?.LabelWidthXs != 0 ? FormContext?.LabelWidthXs : 12));
            SetOffset ??= !FormGroupContext?.HasLabel ?? true;
        }

    }

}
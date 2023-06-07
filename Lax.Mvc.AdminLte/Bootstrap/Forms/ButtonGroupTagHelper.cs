using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Lax.Mvc.AdminLte.Bootstrap.Navigation;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Forms {

    [RestrictChildren("button", "a", "dropdown")]
    [OutputElementHint("div")]
    [ContextClass]
    public class ButtonGroupTagHelper : BootstrapTagHelper {

        [Context]
        protected ButtonToolbarTagHelper ButtonToolbarContext { get; set; }

        public Size? Size { get; set; }
        public ButtonContext? Context { get; set; }

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool Vertical { get; set; }

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool Justified { get; set; }

        public override void Init(TagHelperContext context) {
            base.Init(context);
            if (ButtonToolbarContext == null) {
                return;
            }

            Vertical = false;
            Justified = false;
            if (!Size.HasValue) {
                Size = ButtonToolbarContext.Size;
            }

            if (!Context.HasValue) {
                Context = ButtonToolbarContext.Context;
            }
        }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.Attributes.Add("role", "group");
            if (context.HasContextItem<InputGroupTagHelper>()) {
                Size = Bootstrap.Size.Default;
                if (!context.HasContextItem<AddonTagHelper>()) {
                    output.TagName = "span";
                    output.AddCssClass("input-group-btn");
                }

                context.RemoveContextItem<InputGroupTagHelper>();
            } else {
                output.TagName = "div";
                output.AddCssClass(Vertical ? "btn-group-vertical" : "btn-group");

                if (Size.HasValue) {
                    output.AddCssClass("btn-group-" + Size.Value.GetDescription());
                }

                if (Justified) {
                    output.AddCssClass("btn-group-justified");
                }
            }
        }

    }

}
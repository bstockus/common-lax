using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.ListGroup {

    [HtmlTargetElement("list-group-item", ParentTag = "list-group")]
    [OutputElementHint("li")]
    public class ListGroupItemTagHelper : BootstrapTagHelper {

        public ListGroupContext? Context { get; set; }

        [HtmlAttributeNotBound]
        [Context]
        public ListGroupTagHelper ListGroupContext { get; set; }

        public string BadgeText { get; set; }

        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Active { get; set; }

        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Disabled { get; set; }

        public override void Init(TagHelperContext context) {
            base.Init(context);
            if (Context == null) {
                Context = ListGroupContext.Context;
            }
        }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            if (ListGroupContext.ChildDetectionMode) {
                output.SuppressOutput();
            } else {
                RenderOutput(output);
            }
        }

        protected virtual void RenderOutput(TagHelperOutput output) {
            output.TagName = GetTagName();
            output.AddCssClass("list-group-item");
            if (!string.IsNullOrEmpty(BadgeText)) {
                output.PreContent.PrependHtml($"<span class=\"badge\">{BadgeText}</span>");
            }

            if (Context != null) {
                output.AddCssClass("list-group-item-" + Context.ToString().ToLower());
            }

            if (Active) {
                output.AddCssClass("active");
            }

            if (Disabled) {
                output.AddCssClass("disabled");
            }
        }

        protected virtual string GetTagName() => ListGroupContext.RenderAsDiv ? "div" : "li";

    }

}
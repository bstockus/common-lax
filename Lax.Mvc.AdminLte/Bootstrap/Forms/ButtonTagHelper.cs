using System;
using System.Threading.Tasks;
using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Lax.Mvc.AdminLte.Bootstrap.Navigation;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Forms {

    [HtmlTargetElement("a", Attributes = ButtonAttributeName)]
    [HtmlTargetElement("a", Attributes = LinkButtonAttributeName)]
    [HtmlTargetElement("a", Attributes = RemoteAttributeName)]
    [HtmlTargetElement("input", Attributes = TypeAttributeName)]
    [HtmlTargetElement("button")]
    [HtmlTargetElement("a", ParentTag = "button-group")]
    [HtmlTargetElement("a", ParentTag = "button-toolbar")]
    [HtmlTargetElement("button", ParentTag = "button-group")]
    [HtmlTargetElement("button", ParentTag = "button-toolbar")]
    public class ButtonTagHelper : BootstrapTagHelper {

        public const string TypeAttributeName = "type";
        public const string ButtonAttributeName = AttributePrefix + "button";
        public const string LinkButtonAttributeName = AttributePrefix + "link-button";
        public const string ContextAttributeName = AttributePrefix + "context";
        public const string SizeAttributeName = AttributePrefix + "size";
        public const string BlockStyleAttrbibuteName = AttributePrefix + "block-style";
        public const string PressesAttributeName = AttributePrefix + "pressed";
        public const string DisabledAttributeName = "disabled";
        public const string RemoteAttributeName = AttributePrefix + "remote";

        [HtmlAttributeName(TypeAttributeName)]
        public string Type { get; set; }

        [HtmlAttributeName(ButtonAttributeName)]
        [HtmlAttributeMinimizable]
        public bool Button { get; set; }

        [HtmlAttributeName(LinkButtonAttributeName)]
        [HtmlAttributeMinimizable]
        public bool LinkButton { get; set; }

        [HtmlAttributeName(ContextAttributeName)]
        public ButtonContext? Context { get; set; }

        [HtmlAttributeName(SizeAttributeName)]
        public Size? Size { get; set; }

        [HtmlAttributeName(BlockStyleAttrbibuteName)]
        [HtmlAttributeMinimizable]
        public bool BlockStyle { get; set; }

        [HtmlAttributeName(PressesAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Pressed { get; set; }

        [HtmlAttributeName(DisabledAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        [CopyToOutput]
        public bool Disabled { get; set; }

        [HtmlAttributeName(RemoteAttributeName)]
        [HtmlAttributeMinimizable]
        public bool Remote { get; set; }

        private bool WrapInButtonGroup { get; set; }

        private bool ButtonGroupJustified { get; set; }

        public override void Init(TagHelperContext context) {
            base.Init(context);
            if (context.HasContextItem<ButtonGroupTagHelper>()) {
                var buttonGroupContext = context.GetContextItem<ButtonGroupTagHelper>();
                Button = true;
                ButtonGroupJustified = buttonGroupContext.Justified;
                Size = ButtonGroupJustified ? buttonGroupContext.Size : Bootstrap.Size.Default;
                if (!Context.HasValue) {
                    Context = buttonGroupContext.Context;
                }
            } else if (context.HasContextItem<ButtonToolbarTagHelper>()) {
                var buttonToolbarContext = context.GetContextItem<ButtonToolbarTagHelper>();
                Button = true;
                WrapInButtonGroup = true;
                if (!Context.HasValue) {
                    Context = buttonToolbarContext.Context;
                }

                if (!Size.HasValue) {
                    Size = buttonToolbarContext.Size;
                }
            }
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
            Type = Type?.ToLower() ?? "";
            output.TagName = output.TagName.ToLower();
            if (Button || LinkButton || output.TagName == "button" ||
                output.TagName == "input" && (Type == "button" || Type == "submit" || Type == "reset")) {
                await base.ProcessAsync(context, output);
            }
        }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            if (!LinkButton) {
                output.AddCssClass("btn");
                output.AddCssClass("btn-" + (Context ?? ButtonContext.Default).ToString().ToLower());
            }

            if (context.HasContextItem<InputGroupTagHelper>()) {
                Size = Bootstrap.Size.Default;
                if (!context.HasContextItem<AddonTagHelper>()) {
                    output.PreElement.PrependHtml("<span class=\"input-group-btn\">");
                    output.PostElement.AppendHtml("</span>");
                }
            }

            if (WrapInButtonGroup ||
                !output.TagName.Equals("a", StringComparison.CurrentCultureIgnoreCase) && ButtonGroupJustified) {
                if (Size.HasValue && Size.Value != Bootstrap.Size.Default) {
                    output.PreElement.SetHtmlContent(
                        $"<div class=\"btn-group btn-group-{Size.Value.GetDescription()}\" role=\"group\">");
                } else {
                    output.PreElement.SetHtmlContent(
                        "<div class=\"btn-group\" role=\"group\">");
                }

                output.PostElement.SetHtmlContent("</div>");
            } else if (Size.HasValue && Size.Value != Bootstrap.Size.Default) {
                output.AddCssClass("btn-" + Size.Value.GetDescription());
            }

            if (BlockStyle) {
                output.AddCssClass("btn-block");
            }

            if (Pressed) {
                output.AddAriaAttribute("pressed", "true");
                output.AddCssClass("active");
            }

            if (Remote) {
                output.AddCssClass("remote-modal-btn");
            }

            if (Disabled) {
                if (output.TagName == "a") {
                    output.AddCssClass("disabled");
                    if (context.AllAttributes.ContainsName("href")) {
                        output.Attributes.RemoveAll("href");
                        output.Attributes.Add("data-href", context.AllAttributes["href"].Value);
                    }
                }

                output.MergeAttribute("role", "button");
            }
        }

    }

}
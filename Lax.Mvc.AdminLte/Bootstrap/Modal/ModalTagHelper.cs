using System.Threading.Tasks;
using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Lax.Mvc.AdminLte.Bootstrap.Forms;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Modal {

    [ContextClass]
    [HtmlTargetElement("bs-modal")]
    public class ModalTagHelper : BootstrapTagHelper {

        protected override bool CopyAttributesIfBootstrapIsDisabled => true;
        public string Header { get; set; }

        [HtmlAttributeNotBound]
        public string HeaderHtml { get; set; }

        public string Footer { get; set; }

        [HtmlAttributeNotBound]
        public string FooterHtml { get; set; }

        public string ToggleButtonText { get; set; }

        public ButtonContext? ToggleButtonContext { get; set; }

        public SimpleSize Size { get; set; }

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool Fade { get; set; }

        [AutoGenerateId]
        [CopyToOutput]
        public string Id { get; set; }

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool NoBackdrop { get; set; }

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool StaticBackdrop { get; set; }

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool NoKeyboard { get; set; }

        [HtmlAttributeMinimizable]
        [HtmlAttributeNotBound]
        public bool NoShow { get; set; }

        protected override async Task BootstrapProcessAsync(TagHelperContext context, TagHelperOutput output) {
            await output.LoadChildContentAsync();
            if (string.IsNullOrEmpty(HeaderHtml) && !string.IsNullOrEmpty(Header)) {
                await new ModalHeaderTagHelper().RunTagHelperAsync(new TagHelperExtensions.Options {
                    Context = context,
                    Content = new DefaultTagHelperContent().AppendHtml(Header)
                });
            }

            if (string.IsNullOrEmpty(FooterHtml) && !string.IsNullOrEmpty(Footer)) {
                await new ModalFooterTagHelper().RunTagHelperAsync(new TagHelperExtensions.Options {
                    Context = context,
                    Content = new DefaultTagHelperContent().AppendHtml(Footer)
                });
            }

            if (!string.IsNullOrEmpty(ToggleButtonText)) {
                output.PreElement.AppendHtml(
                    await new ITagHelper[] {
                        new ButtonTagHelper {Context = ToggleButtonContext},
                        new ModalToggleTagHelper {ModalTarget = Id}
                    }.ToTagHelperContentAsync(
                        new TagHelperExtensions.Options {
                            Content =
                                new DefaultTagHelperContent
                                    ().AppendHtml(
                                    ToggleButtonText),
                            TagName = "button"
                        }));
            }

            output.TagName = "div";
            output.AddCssClass("modal");
            output.Attributes.Add("tabindex", "-1");
            if (NoBackdrop) {
                output.Attributes.AddDataAttribute("backdrop", "false");
            }

            if (StaticBackdrop) {
                output.Attributes.AddDataAttribute("backdrop", "static");
            }

            if (NoKeyboard) {
                output.Attributes.AddDataAttribute("keyboard", "false");
            }

            if (NoShow) {
                output.Attributes.AddDataAttribute("show", "false");
            }

            if (Fade) {
                output.AddCssClass("fade");
            }

            output.Attributes.Add("role", "dialog");
            output.PreContent.AppendHtml(Size == SimpleSize.Default
                ? "<div class=\"modal-dialog\" role=\"document\">"
                : $"<div class=\"modal-dialog modal-{Size.GetDescription()}\" role=\"document\">");

            output.PreContent.AppendHtml($"<div class=\"modal-content\">{HeaderHtml}<div class=\"modal-body\">");
            output.PostContent.AppendHtml($"</div>{FooterHtml}</div></div>");
        }

    }

}
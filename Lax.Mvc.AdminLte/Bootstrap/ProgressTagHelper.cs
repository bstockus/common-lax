using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap {

    [RestrictChildren(ProgressBarTagHelper.ProgressBarTagName)]
    [ContextClass]
    public class ProgressTagHelper : BootstrapTagHelper {

        public const string DisplayValueAttributeName = AttributePrefix + "display-value";
        public const string StripedAttributeName = AttributePrefix + "striped";
        public const string AnimatedAttributeName = AttributePrefix + "animated";
        public const string SrTextAttributeName = AttributePrefix + "sr-text";

        [HtmlAttributeName(DisplayValueAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool? DisplayValue { get; set; }

        [HtmlAttributeName(StripedAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool? Striped { get; set; }

        [HtmlAttributeName(AnimatedAttributeName)]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool? Animated { get; set; }

        [HtmlAttributeName(SrTextAttributeName)]
        public string SrText { get; set; }


        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "div";
            output.AddCssClass("progress");
        }

    }

}
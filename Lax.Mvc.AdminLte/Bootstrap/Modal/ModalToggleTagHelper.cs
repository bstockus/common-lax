using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Modal {

    [HtmlTargetElement("*", Attributes = ModalTargetAttributeName)]
    public class ModalToggleTagHelper : BootstrapTagHelper {

        public const string ModalTargetAttributeName = AttributePrefix + "modal-target";

        [HtmlAttributeName(ModalTargetAttributeName)]
        public string ModalTarget { get; set; }

        protected override void BootstrapProcess(TagHelperContext context, TagHelperOutput output) {
            output.Attributes.AddDataAttribute("target", "#" + ModalTarget);
            output.Attributes.AddDataAttribute("toggle", "modal");
        }

    }

}
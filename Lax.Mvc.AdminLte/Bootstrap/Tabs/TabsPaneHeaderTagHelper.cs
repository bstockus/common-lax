using System.Threading.Tasks;
using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Tabs {

    [HtmlTargetElement("header", ParentTag = "pane")]
    [HtmlTargetElement("header", ParentTag = "pane-group")]
    public class TabsPaneHeaderTagHelper : BootstrapTagHelper {

        [HtmlAttributeNotBound]
        [Context]
        public TabsPaneTagHelper PanelContext { get; set; }

        protected override async Task BootstrapProcessAsync(TagHelperContext context, TagHelperOutput output) {
            PanelContext.HeaderHtml = (await output.GetChildContentAsync()).GetContent();
            output.SuppressOutput();
        }

    }

}
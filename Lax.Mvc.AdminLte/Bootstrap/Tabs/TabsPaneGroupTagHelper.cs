using System.Collections.Generic;
using System.Threading.Tasks;
using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Tabs {

    [RestrictChildren("pane", "header")]
    [HtmlTargetElement("pane-group", ParentTag = "tabs")]
    [ContextClass]
    public class TabsPaneGroupTagHelper : TabsPaneTagHelper {

        public List<TabsPaneTagHelper> Panes { get; set; } = new List<TabsPaneTagHelper>();

        public override void Init(TagHelperContext context) {
            base.Init(context);
            TabsContext.ActiveIndex++;
        }

        protected override async Task BootstrapProcessAsync(TagHelperContext context, TagHelperOutput output) {
            await base.BootstrapProcessAsync(context, output);
            output.TagName = null;
        }

        public override void WrapHeaderHtml() {
            HeaderHtml = Active
                ? $"<li role=\"presentation\" class=\"dropdown active\"><a href=\"#\" id=\"{Id}\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" aria-controls=\"{Id}-contents\" aria-expanded=\"false\">{HeaderHtml} <span class=\"caret\"></span></a>"
                : $"<li role=\"presentation\" class=\"dropdown\"><a href=\"#\" id=\"{Id}\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" aria-controls=\"{Id}-contents\" aria-expanded=\"false\">{HeaderHtml} <span class=\"caret\"></span></a>";
            HeaderHtml += $"<ul class=\"dropdown-menu\" aria-labelledby=\"{Id}\" id=\"{Id}-contents\">";
            foreach (var pane in Panes) {
                HeaderHtml += pane.HeaderHtml;
            }

            HeaderHtml += "</ul></li>";
        }

    }

}
using System.Threading.Tasks;
using Lax.Mvc.AdminLte.Bootstrap.Attributes;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Tabs {

    [HtmlTargetElement("pane", ParentTag = "tabs")]
    [HtmlTargetElement("pane", ParentTag = "pane-group")]
    [ContextClass]
    public class TabsPaneTagHelper : BootstrapTagHelper {

        [HtmlAttributeNotBound]
        public string HeaderHtml { get; set; }

        public string Header { get; set; }

        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool Active { get; set; }

        [AutoGenerateId]
        [CopyToOutput]
        public string Id { get; set; }

        protected override bool CopyAttributesIfBootstrapIsDisabled => true;

        [Context]
        protected TabsTagHelper TabsContext { get; set; }

        [HtmlAttributeNotBound]
        protected string DataToggleTarget { get; set; }

        public override void Init(TagHelperContext context) {
            base.Init(context);
            if (TabsContext.ActiveIndex == TabsContext.CurrentIndex) {
                Active = true;
            }

            if (context.HasContextItem<TabsPaneGroupTagHelper>()) {
                var paneGroupContext = context.GetContextItem<TabsPaneGroupTagHelper>();
                paneGroupContext.Panes.Add(this);
                if (Active) {
                    paneGroupContext.Active = true;
                }
            } else {
                TabsContext.Panes.Add(this);
            }

            DataToggleTarget = TabsContext.Pills ? "pill" : "tab";
        }

        protected override async Task BootstrapProcessAsync(TagHelperContext context, TagHelperOutput output) {
            await output.GetChildContentAsync();
            if (string.IsNullOrEmpty(HeaderHtml)) {
                HeaderHtml = Header;
            }

            WrapHeaderHtml();
            output.TagName = "div";
            output.Attributes.Add("role", "tabpanel");
            output.AddCssClass("tab-pane");
            if (Active) {
                output.AddCssClass("active");
            }

            if (TabsContext.Fade) {
                output.AddCssClass("fade");
                if (Active) {
                    output.AddCssClass("in");
                }
            }
        }

        public virtual void WrapHeaderHtml() =>
            HeaderHtml = Active
                ? $"<li role=\"presentation\" class=\"active\"><a href=\"#{Id}\" aria-controls=\"{Id}\" role=\"tab\" data-toggle=\"{(TabsContext.Pills ? "pill" : "tab")}\">{HeaderHtml}</a></li>"
                : $"<li role=\"presentation\"><a href=\"#{Id}\" aria-controls=\"{Id}\" role=\"tab\" data-toggle=\"{(TabsContext.Pills ? "pill" : "tab")}\">{HeaderHtml}</a></li>";

    }

}
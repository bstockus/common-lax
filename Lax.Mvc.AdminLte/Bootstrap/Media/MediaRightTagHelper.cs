using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Media {

    [OutputElementHint("img")]
    [HtmlTargetElement("media-right", TagStructure = TagStructure.WithoutEndTag)]
    public class MediaRightTagHelper : MediaLeftTagHelper {

        protected override string CssClass => "media-right";

    }

}
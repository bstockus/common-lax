using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Md6TagHelper : SizedColTagHelper {

        protected override int Size => 6;
        protected override string Type => "md";

    }

}